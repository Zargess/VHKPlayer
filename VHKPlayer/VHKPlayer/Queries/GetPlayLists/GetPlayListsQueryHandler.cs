using System.Linq;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetPlayLists
{
    class GetPlayListsQueryHandler : IQueryHandler<GetPlayListsQuery, IQueryable<PlayList>>
    {
        private readonly IDataCenter _center;

        public GetPlayListsQueryHandler(IDataCenter center)
        {
            this._center = center;
        }

        public IQueryable<PlayList> Handle(GetPlayListsQuery query)
        {
            return _center.PlayLists.AsQueryable();
        }
    }
}