using VHKPlayer.Controllers.Interfaces;
using VHKPlayer.Models;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility.FileDelayStrategy.Interfaces;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Utility.HandleFile.Interfaces
{
    public interface IHandleFileStrategy
    {
        void Handle(FileNode file, IVideoPlayerController controller, IPlayStrategy playStrategy,
            IQueryProcessor processor, IFileDelayStrategy delay);
    }
}