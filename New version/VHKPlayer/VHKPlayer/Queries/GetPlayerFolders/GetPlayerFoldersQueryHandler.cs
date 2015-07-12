using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetPlayerFolders
{
    class GetPlayerFoldersQueryHandler : IQueryHandler<GetPlayerFoldersQuery, IQueryable<FolderNode>>
    {
        private readonly IQueryProcessor processor;

        // TODO : Make a way to get information from settings in an easy way
        public GetPlayerFoldersQueryHandler(IQueryProcessor processor)
        {
            this.processor = processor;
        }

        public IQueryable<FolderNode> Handle(GetPlayerFoldersQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
