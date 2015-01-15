using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Enums;
using VHKPlayer.Interfaces;

namespace VHKPlayer.Strategies.Loading.PlayLists {
    public class SortedLoadingStrategy : ILoadingStrategy<IFile> {
        public int Index { get; private set; }
        public IFolder Folder { get; private set; }

        public SortedLoadingStrategy(int index, IFolder folder) {
            Index = index;
            Folder = folder;
        }

        public void Load(ICollection<IFile> content) {
            content.Clear();

            foreach (var file in Folder.Content) {
                var c = file.Name[Index - 1].ToString();
                if (!file.Exists()) continue;
                if (file.Type == FileType.Unsupported) continue;
                if (ToNumber(c) <= 0) continue;
                content.Add(file);
            }
        }

        private int ToNumber(string c) {
            int n = 0;
            int.TryParse(c, out n);
            return n;
        }
    }
}