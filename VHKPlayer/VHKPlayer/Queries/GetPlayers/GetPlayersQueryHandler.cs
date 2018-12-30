using System.Linq;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetPlayers
{
    class GetPlayersQueryHandler : IQueryHandler<GetPlayersQuery, IQueryable<Player>>
    {
        private readonly IDataCenter _center;

        public GetPlayersQueryHandler(IDataCenter center)
        {
            this._center = center;
        }

        public IQueryable<Player> Handle(GetPlayersQuery query)
        {
            return _center.Players.AsQueryable();
        }
    }
}