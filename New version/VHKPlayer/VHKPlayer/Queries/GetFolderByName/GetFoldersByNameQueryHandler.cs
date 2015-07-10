using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models;
using VHKPlayer.Queries.GetFolders;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetFolderByName
{
    class GetFoldersByNameQueryHandler : IQueryHandler<GetFolderByNameQuery, FolderNode>
    {
        private readonly IQueryProcessor processor;

        public GetFoldersByNameQueryHandler(IQueryProcessor processor)
        {
            this.processor = processor;
        }

        public FolderNode Handle(GetFolderByNameQuery query)
        {
            var folders = processor.Process(new GetFoldersQuery());
            var folder = folders.SingleOrDefault(x => x.Name == query.Name);
            return folder;
        }
    }
}
