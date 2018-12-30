using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Commands.Logic.RemovePlayableFile
{
    class RemovePlayableFileCommandHandler : ICommandHandler<RemovePlayableFileCommand>
    {
        private readonly IDataCenter _center;

        public RemovePlayableFileCommandHandler(IDataCenter center)
        {
            this._center = center;
        }

        public void Handle(RemovePlayableFileCommand command)
        {
            _center.PlayableFiles.Remove(command.PlayableFile);
        }
    }
}