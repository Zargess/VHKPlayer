using System.Collections.Generic;
using VHKPlayer.Exceptions;
using VHKPlayer.Models;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility.LoadingStrategy.Interfaces;
using VHKPlayer.Utility.LoadingStrategy.PlayListLoading;

namespace VHKPlayer.Queries.GetPlayListLoadingStrategy
{
    class GetPlayListLoadingStrategyQueryHandler : IQueryHandler<GetPlayListLoadingStrategyQuery,
        ILoadingStrategy<ICollection<FileNode>>>
    {
        public ILoadingStrategy<ICollection<FileNode>> Handle(GetPlayListLoadingStrategyQuery query)
        {
            if (query.StrategyName == "Folder")
            {
                return new FolderLoadingStrategy(query.Folder);
            }

            if (query.StrategyName == "Sorted")
            {
                return new SortedLoadingStrategy(query.Index, query.Folder);
            }

            throw new NoSuchLoadingStrategyException();
        }
    }
}