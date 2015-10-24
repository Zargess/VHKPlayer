using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetPlayLists
{
    class GetPlayListsQueryHandler : IQueryHandler<GetPlayListsQuery, IQueryable<PlayList>>
    {
        private readonly IDataCenter center;

        public GetPlayListsQueryHandler(IDataCenter center)
        {
            this.center = center;
        }

        public IQueryable<PlayList> Handle(GetPlayListsQuery query)
        {
            return center.PlayLists.AsQueryable();
        }
    }
}
