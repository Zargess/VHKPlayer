using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Infrastructure;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Queries.GetPlayerFolders;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Commands.Logic.CreatePlayer
{
    class CreatePlayerCommandHandler : ICommandHandler<CreatePlayerCommand>
    {
        private readonly IDataCenter _center;
        private readonly IQueryProcessor _processor;

        public CreatePlayerCommandHandler(IQueryProcessor processor, IDataCenter center)
        {
            this._processor = processor;
            this._center = center;
        }

        public void Handle(CreatePlayerCommand command)
        {
            var name = command.File.NameWithoutExtension.Remove(0, 6);
            var number = command.File.NameWithoutExtension.Substring(0, 3).ToInteger();
            var trainer = number >= 90;

            var content = new List<FileNode>();

            var folders = _processor.Process(new GetPlayerFoldersQuery());

            foreach (var folder in folders)
            {
                if (folder == null) continue;
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
                Processor = _processor
            };

            command.Folder.AddObserver(player);

            _center.Players.Add(player);
        }
    }
}