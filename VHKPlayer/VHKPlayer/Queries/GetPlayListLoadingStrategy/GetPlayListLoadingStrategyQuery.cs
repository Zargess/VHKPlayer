using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
