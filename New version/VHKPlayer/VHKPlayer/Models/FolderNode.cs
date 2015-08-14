using System.Collections.Generic;
using System.IO;
using VHKPlayer.Commands.Logic.CreateFile;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Models
{
    // TODO : Do some cleaning in this class and test it
    public class FolderNode
    {
        private FileSystemWatcher watcher;
        private string fullPath;
        private ICommandProcessor processor;

        public string Name { get; private set; }
        public List<FileNode> Content { get; set; }
        public List<IVHKObserver<FolderNode>> Observers { get; private set; }

        public string FullPath
        {
            get
            {
                return fullPath;
            }
            set
            {
                fullPath = value;
                UpdateFolderInfo(value);
            }
        }


        public FolderNode(ICommandProcessor processor)
        {
            Observers = new List<IVHKObserver<FolderNode>>();
            this.processor = processor;
            Content = new List<FileNode>();
        }

        private void UpdateFolderInfo(string value)
        {
            if (!Exists()) return;
            Name = Path.GetDirectoryName(value);

            if (watcher != null) watcher.Dispose();

            watcher = CreateWatcher(value);
            CreateFiles(value);
        }

        private void CreateFiles(string value)
        {
            Content.Clear();
            if (!Exists()) return;
            var paths = Directory.EnumerateFiles(value);
            foreach (var path in paths)
            {
                processor.Process(new CreateFileCommand()
                {
                    Folder = this,
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

        public bool Contains(FileNode file)
        {
            foreach (var f in Content)
            {
                if (f.FullPath.ToLower().Equals(file.FullPath.ToLower()))
                {
                    return true;
                }
            }

            return false;
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
            Observers.Add(observer);
        }

        public void RemoveObserver(IVHKObserver<FolderNode> observer)
        {
            Observers.Remove(observer);
        }

        private void Changed(object sender, FileSystemEventArgs e)
        {
            Content.Clear();
            CreateFiles(FullPath);
            Observers.ForEach(x => x.SubjectUpdated(this));
        }
    }
}