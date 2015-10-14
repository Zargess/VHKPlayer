using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Queries.GetPlayableFiles;
using VHKPlayer.Queries.GetPlayers;
using VHKPlayer.Queries.GetPlayLists;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetAllPlayables
{
    class GetAllPlayablesQueryHandler : IQueryHandler<GetAllPlayablesQuery, IQueryable<IPlayable>>
    {
        private readonly IQueryProcessor processor;

        public GetAllPlayablesQueryHandler(IQueryProcessor processor)
        {
            this.processor = processor;
        } 

        public IQueryable<IPlayable> Handle(GetAllPlayablesQuery query)
        {
            var res = new List<IPlayable>();
            res.AddRange(processor.Process(new GetPlayersQuery()));
            res.AddRange(processor.Process(new GetPlayListsQuery()));
            res.AddRange(processor.Process(new GetPlayableFilesQuery()));
            return res.AsQueryable();
        }
    }
}
