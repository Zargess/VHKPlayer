using System.Collections.ObjectModel;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Commands.Logic.CreatePlayList
{
    class CreatePlayListCommandHandler : ICommandHandler<CreatePlayListCommand>
    {
        private readonly IDataCenter _center;

        public CreatePlayListCommandHandler(IDataCenter center)
        {
            this._center = center;
        }

        // TODO : Consider using the construction string approach
        public void Handle(CreatePlayListCommand command)
        {
            var playlist = new PlayList()
            {
                Name = command.Name,
                HasAudio = command.HasAudio,
                LoadingStrategy = command.LoadingStrategy,
                PlayStrategy = command.PlayStrategy,
                Content = new ObservableCollection<FileNode>(command.LoadingStrategy.Load())
            };

            command.Folder.AddObserver(playlist);

            _center.PlayLists.Add(playlist);
        }
    }
}