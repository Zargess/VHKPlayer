using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models;
using VHKPlayer.Queries.GetFolders;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetFolderByPathSubscript
{
    class GetFolderByPathSubscriptQueryHandler : IQueryHandler<GetFolderByPathSubscriptQuery, FolderNode>
    {
        private readonly IQueryProcessor processor;

        public GetFolderByPathSubscriptQueryHandler(IQueryProcessor processor)
        {
            this.processor = processor;
        }

        public FolderNode Handle(GetFolderByPathSubscriptQuery query)
        {
            var folders = processor.Process(new GetFoldersQuery()).ToList();
            var folder = folders.SingleOrDefault(x => x.FullPath.ToLower().EndsWith(query.PartialPath.ToLower()));
            return folder;
        }
    }
}
