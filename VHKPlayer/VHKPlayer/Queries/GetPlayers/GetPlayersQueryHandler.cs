using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetPlayers
{
    class GetPlayersQueryHandler : IQueryHandler<GetPlayersQuery, IQueryable<Player>>
    {
        private readonly IDataCenter center;

        public GetPlayersQueryHandler(IDataCenter center)
        {
            this.center = center;
        }

        public IQueryable<Player> Handle(GetPlayersQuery query)
        {
            return center.Players.AsQueryable();
        }
    }
}
