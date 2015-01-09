using System.Collections.Generic;

namespace VHKPlayer.Interfaces {
    public interface IFolder {
        string Name { get; }
        string FullPath { get; }
        List<IFile> Content { get; }

        bool Exists();
        bool ContainsFile(IFile file);
        bool ValidRootFolder();
        void AddObserver(IFolderObserver observer);
        void RemoveObserver(IFolderObserver observer);
    }
}