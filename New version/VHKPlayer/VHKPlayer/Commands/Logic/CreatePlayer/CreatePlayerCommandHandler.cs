using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Infrastructure;
using VHKPlayer.Models;
using VHKPlayer.Utility.GetPlayerFolders.Interfaces;

namespace VHKPlayer.Commands.Logic.CreatePlayer
{
    class CreatePlayerCommandHandler : ICommandHandler<CreatePlayerCommand>
    {
        private readonly DataCenter center;
        private readonly IGetPlayerFoldersStrategy strategy;

        public CreatePlayerCommandHandler(IGetPlayerFoldersStrategy strategy, DataCenter center)
        {
            this.strategy = strategy;
            this.center = center;
        }

        public void Handle(CreatePlayerCommand command)
        {
            var name = command.File.NameWithoutExtension.Remove(0, 6);
            var number = command.File.NameWithoutExtension.Substring(0, 3).ToInteger();
            var trainer = number >= 90;

            var content = new List<FileNode>();

            foreach (var folder in strategy.GetFolders())
            {
                var file = folder.Content.SingleOrDefault(x => x.NameWithoutExtension.ToLower() == command.File.NameWithoutExtension.ToLower());
                if (file == null) continue;
                content.Add(file);
            }
            var player = new Player()
            {
                Name = name,
                Number = number,
                Trainer = trainer,
                Content = new ObservableCollection<FileNode>(content),
                StatsLoadingStrategy = command.LoadingStrategy
            };

            command.Folder.AddObserver(player);

            center.AddPlayer(player);
        }
    }
}