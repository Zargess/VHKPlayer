using System.Linq;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Infrastructure;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Commands.Logic.CreateFolder
{
    class CreateFolderCommandHandler : ICommandHandler<CreateFolderCommand>
    {
        private readonly ICommandProcessor _processor;
        private readonly IDataCenter _center;

        public CreateFolderCommandHandler(IDataCenter center, ICommandProcessor processor)
        {
            this._center = center;
            this._processor = processor;
        }

        public void Handle(CreateFolderCommand command)
        {
            if (_center.Folders.Any(x => x.FullPath.ToLower() == command.Path.ToLower())) return;
            _center.Folders.Add(new FolderNode(_processor)
            {
                FullPath = command.Path
            });
        }
    }
}
