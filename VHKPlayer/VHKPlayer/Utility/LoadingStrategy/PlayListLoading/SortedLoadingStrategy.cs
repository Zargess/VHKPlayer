using System.Collections.Generic;
using VHKPlayer.Infrastructure;
using VHKPlayer.Models;
using VHKPlayer.Utility.LoadingStrategy.Interfaces;

namespace VHKPlayer.Utility.LoadingStrategy.PlayListLoading
{
    public class SortedLoadingStrategy : ILoadingStrategy<ICollection<FileNode>>
    {
        private readonly FolderNode _folder;
        private readonly int _index;

        public SortedLoadingStrategy(int index, FolderNode folder)
        {
            this._index = index;
            this._folder = folder;
        }

        public ICollection<FileNode> Load()
        {
            var res = new List<FileNode>();
            if (_folder == null) return res;

            foreach (var file in _folder.Content)
            {
                var c = file.Name[_index - 1].ToString();
                if (!file.Exists()) continue;
                if (file.Type == FileType.Unsupported) continue;
                if (c.ToInteger() <= 0) continue;
                res.Add(file);
            }

            return res;
        }
    }
}