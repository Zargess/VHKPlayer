using VHKPlayer.Enums;

namespace VHKPlayer.Interfaces {
    public interface IFile {
        string Name { get; }
        string FullPath { get; }
        string NameWithoutExtension { get; }
        FileType Type { get; }

        bool Exists();
    }
}