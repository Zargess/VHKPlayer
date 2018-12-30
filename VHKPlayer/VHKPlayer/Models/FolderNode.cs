using System;
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
        private FileSystemWatcher _watcher;
        private string _fullPath;
        private ICommandProcessor _processor;

        public string Name { get; private set; }
        public List<FileNode> Content { get; set; }
        public List<IVhkObserver<FolderNode>> Observers { get; private set; }

        public string FullPath
        {
            get { return _fullPath; }
            set
            {
                _fullPath = value;
                UpdateFolderInfo(value);
            }
        }


        public FolderNode(ICommandProcessor processor)
        {
            Observers = new List<IVhkObserver<FolderNode>>();
            this._processor = processor;
            Content = new List<FileNode>();
        }

        private void UpdateFolderInfo(string value)
        {
            if (!Exists()) return;
            Name = Path.GetDirectoryName(value);

            if (_watcher != null) _watcher.Dispose();

            _watcher = CreateWatcher(value);
            CreateFiles(value);
        }

        private void CreateFiles(string value)
        {
            Content.Clear();
            if (!Exists()) return;
            var paths = Directory.EnumerateFiles(value);
            foreach (var path in paths)
            {
                _processor.Process(new CreateFileCommand()
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
            if (file == null) return false;
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

        public void AddObserver(IVhkObserver<FolderNode> observer)
        {
            Observers.Add(observer);
        }

        public void RemoveObserver(IVhkObserver<FolderNode> observer)
        {
            Observers.Remove(observer);
        }

        private void UpdateFolder()
        {
            Content.Clear();
            CreateFiles(FullPath);
            Observers.ForEach(x => x.SubjectUpdated(this));
        }

        private void Changed(object sender, FileSystemEventArgs e)
        {
            App.Dispatch.BeginInvoke(new Action(() => UpdateFolder()));
        }
    }
}