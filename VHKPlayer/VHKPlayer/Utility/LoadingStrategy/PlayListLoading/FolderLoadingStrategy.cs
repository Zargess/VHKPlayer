using System.Collections.Generic;
using VHKPlayer.Models;
using VHKPlayer.Utility.LoadingStrategy.Interfaces;

namespace VHKPlayer.Utility.LoadingStrategy.PlayListLoading
{
    public class FolderLoadingStrategy : ILoadingStrategy<ICollection<FileNode>>
    {
        private readonly FolderNode folder;

        public FolderLoadingStrategy(FolderNode folder)
        {
            this.folder = folder;
        }

        public ICollection<FileNode> Load()
        {
            var res = new List<FileNode>();

            foreach (var file in folder.Content)
            {
                if (!file.Exists()) continue;
                if (file.Type == FileType.Unsupported) continue;
                res.Add(file);
            }

            return res;
        }
    }
}