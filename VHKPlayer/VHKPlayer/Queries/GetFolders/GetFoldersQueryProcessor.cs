using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetFolders
{
    class GetFoldersQueryHandler : IQueryHandler<GetFoldersQuery, IQueryable<FolderNode>>
    {
        private readonly IDataCenter _center;

        public GetFoldersQueryHandler(IDataCenter center)
        {
            this._center = center;
        }

        public IQueryable<FolderNode> Handle(GetFoldersQuery query)
        {
            return _center.Folders.AsQueryable();
        }
    }
}
