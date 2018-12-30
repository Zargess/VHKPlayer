using System.Collections.ObjectModel;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Commands.Logic.CreatePlayableFile
{
    class CreatePlayableFileCommandHandler : ICommandHandler<CreatePlayableFileCommand>
    {
        private readonly IDataCenter _center;

        public CreatePlayableFileCommandHandler(IDataCenter center)
        {
            this._center = center;
        }

        public void Handle(CreatePlayableFileCommand command)
        {
            _center.PlayableFiles.Add(new PlayableFile()
            {
                Name = command.File.Name,
                File = command.File,
                Content = new ObservableCollection<FileNode>()
                {
                    command.File
                }
            });
        }
    }
}