using VHKPlayer.Models;

namespace VHKPlayer.Utility.FindFileType.Interfaces
{
    public interface IFindFileTypeStrategy
    {
        FileType FindType(string path);
    }
}