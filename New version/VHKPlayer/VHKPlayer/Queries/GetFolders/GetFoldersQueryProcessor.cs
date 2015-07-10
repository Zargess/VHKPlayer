using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetFolders
{
    class GetFoldersQueryProcessor : IQueryHandler<GetFoldersQuery, ICollection<FolderNode>>
    {
        private readonly DataCenter center;

        public GetFoldersQueryProcessor(DataCenter center)
        {
            this.center = center;
        }

        public ICollection<FolderNode> Handle(GetFoldersQuery query)
        {
            return center.Folders;
        }
    }
}
