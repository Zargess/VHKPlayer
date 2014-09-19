using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.FileManagement {
    public class PlayList {
        public List<FileNode> Content { get; private set; }
        public string Name { get; set; }

        public PlayList(string name) {
            Name = name;
            Content = new List<FileNode>();
        }

        public PlayList(string name, List<FileNode> content) : this(name) {
            AddRange(content);
        }

        public void AddRange(List<FileNode> list) {
            list.ForEach(x => Content.Add(new FileNode(x.FullPath)));
        }

        public void Add(FileNode file) {
            Content.Add(new FileNode(file.FullPath));
        }

        public FileNode GetFile(int index) {
            return Content[index];
        }
    }
}