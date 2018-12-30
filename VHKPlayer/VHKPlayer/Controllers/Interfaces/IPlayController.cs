using VHKPlayer.Models;

namespace VHKPlayer.Controllers.Interfaces
{
    public interface IPlayController
    {
        MediaPlayerState VideoState { get; }
        MediaPlayerState AudioState { get; }
        void Mute(FileType type);
        void Pause(FileType type);
        void Play(FileNode file);
        void Resume(FileType type);
        void ShowStats(Player p);
        void Stop(FileType type);
    }
}