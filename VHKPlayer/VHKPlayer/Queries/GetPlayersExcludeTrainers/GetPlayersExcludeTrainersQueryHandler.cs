using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models;
using VHKPlayer.Queries.GetPlayers;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetPlayersExcludeTrainers
{
    class GetPlayersExcludeTrainersQueryHandler : IQueryHandler<GetPlayersExcludeTrainersQuery, IQueryable<Player>>
    {
        private readonly IQueryProcessor _processor;

        public GetPlayersExcludeTrainersQueryHandler(IQueryProcessor processor)
        {
            this._processor = processor;
        }

        public IQueryable<Player> Handle(GetPlayersExcludeTrainersQuery query)
        {
            return _processor.Process(new GetPlayersQuery()).Where(x => !x.Trainer);
        }
    }
}
