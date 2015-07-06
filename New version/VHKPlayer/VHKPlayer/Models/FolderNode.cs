using System.Collections.Generic;
using System.IO;
using VHKPlayer.Commands.Logic.CreateFile;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Utility.IsValidRootFolder.Interfaces;

namespace VHKPlayer.Models
{
    // TODO : Do some cleaning in this class and test it
    public class FolderNode
    {
        public IValidRootFolderStrategy ValidRootFolder { get; set; }
        private FileSystemWatcher watcher;
        private List<IVHKObserver<FolderNode>> observers;
        public List<FileNode> Content { get; private set; }
        private string fullPath;
        private ICommandProcessor processor;

        public string FullPath
        {
            get
            {
                return fullPath;
            }
            set
            {
                fullPath = value;
                updateFolderInfo(value);
            }
        }

        public string Name { get; private set; }

        public FolderNode(ICommandProcessor processor)
        {
            observers = new List<IVHKObserver<FolderNode>>();
            this.processor = processor;
            Content = new List<FileNode>();
        }

        private void updateFolderInfo(string value)
        {
            Name = Path.GetDirectoryName(value);
            watcher = CreateWatcher(value);
            CreateFiles(value);
        }

        private void CreateFiles(string value)
        {
            if (!Exists()) return;
            var paths = Directory.EnumerateFiles(value);
            foreach (var path in paths)
            {
                processor.Process(new CreateFileCommand()
                {
                    Path = path
                });
            }
        }

        public void AddFile(FileNode node)
        {
            Content.Add(node);
        }

        public bool Exists()
        {
            return Directory.Exists(FullPath);
        }

        private FileSystemWatcher CreateWatcher(string path)
        {
            if (!Exists()) return null;
            var watcher = new FileSystemWatcher
            {
                Path = FullPath,
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastAccess
                    | NotifyFilters.LastWrite | NotifyFilters.DirectoryName,
                Filter = "*"
            };
            watcher.Created += Changed;
            watcher.Deleted += Changed;
            watcher.Renamed += Changed;
            watcher.EnableRaisingEvents = true;
            return watcher;
        }

        public void AddObserver(IVHKObserver<FolderNode> observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IVHKObserver<FolderNode> observer)
        {
            observers.Remove(observer);
        }

        private void Changed(object sender, FileSystemEventArgs e)
        {
            Content.Clear();
            CreateFiles(FullPath);
            observers.ForEach(x => x.SubjectUpdated(this));
        }
    }
}
