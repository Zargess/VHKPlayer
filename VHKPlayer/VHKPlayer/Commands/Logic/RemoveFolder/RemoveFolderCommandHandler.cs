using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Commands.Logic.RemoveFolder
{
    class RemoveFolderCommandHandler : ICommandHandler<RemoveFolderCommand>
    {
        private readonly IDataCenter _center;

        public RemoveFolderCommandHandler(IDataCenter center)
        {
            this._center = center;
        }

        public void Handle(RemoveFolderCommand command)
        {
            _center.Folders.Remove(command.Folder);
        }
    }
}