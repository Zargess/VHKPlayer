﻿using System.Collections.Generic;
using VHKPlayer.Models;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility.LoadingStrategy.Interfaces;

namespace VHKPlayer.Queries.GetPlayListLoadingStrategy
{
    public class GetPlayListLoadingStrategyQuery : IQuery<ILoadingStrategy<ICollection<FileNode>>>
    {
        public string StrategyName { get; set; }
        public int Index { get; set; }
        public FolderNode Folder { get; set; }
    }
}