﻿using System.Collections.Generic;
using VHKPlayer.Infrastructure;
using VHKPlayer.Models;
using VHKPlayer.Utility.LoadingStrategy.Interfaces;

namespace VHKPlayer.Utility.LoadingStrategy.PlayListLoading
{
    public class SortedLoadingStrategy : ILoadingStrategy<ICollection<FileNode>>
    {
        private readonly FolderNode folder;
        private readonly int index;

        public SortedLoadingStrategy(int index, FolderNode folder)
        {
            this.index = index;
            this.folder = folder;
        }

        public ICollection<FileNode> Load()
        {
            var res = new List<FileNode>();

            foreach (var file in folder.Content)
            {
                var c = file.Name[index - 1].ToString();
                if (!file.Exists()) continue;
                if (file.Type == FileType.Unsupported) continue;
                if (c.ToInteger() <= 0) continue;
                res.Add(file);
            }

            return res;
        }
    }
}
