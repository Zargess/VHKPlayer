using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models;
using VHKPlayer.Queries.GetFolders;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetFolderFromPath
{
    class GetFolderFromPathQueryHandler : IQueryHandler<GetFolderFromPathQuery, FolderNode>
    {
        private readonly IQueryProcessor processor;

        public GetFolderFromPathQueryHandler(IQueryProcessor processor)
        {
            this.processor = processor;
        }

        public FolderNode Handle(GetFolderFromPathQuery query)
        {
            var folders = processor.Process(new GetFoldersQuery());
            return folders.SingleOrDefault(x => x.FullPath.ToLower() == query.Path.ToLower());
        }
    }
}
