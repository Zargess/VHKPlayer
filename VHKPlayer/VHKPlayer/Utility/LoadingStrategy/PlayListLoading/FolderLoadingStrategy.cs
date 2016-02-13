using System.Collections.Generic;
using VHKPlayer.Models;
using VHKPlayer.Utility.LoadingStrategy.Interfaces;

namespace VHKPlayer.Utility.LoadingStrategy.PlayListLoading
{
    public class FolderLoadingStrategy : ILoadingStrategy<ICollection<FileNode>>
    {
        private readonly FolderNode _folder;

        public FolderLoadingStrategy(FolderNode folder)
        {
            this._folder = folder;
        }

        public ICollection<FileNode> Load()
        {
            var res = new List<FileNode>();
            if (_folder == null) return res;
            foreach (var file in _folder.Content)
            {
                if (!file.Exists()) continue;
                if (file.Type == FileType.Unsupported) continue;
                res.Add(file);
            }

            return res;
        }
    }
}