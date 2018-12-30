using System.Collections.Generic;
using System.Linq;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Queries.GetPlayableFiles;
using VHKPlayer.Queries.GetPlayers;
using VHKPlayer.Queries.GetPlayLists;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetAllPlayables
{
    class GetAllPlayablesQueryHandler : IQueryHandler<GetAllPlayablesQuery, IQueryable<IPlayable>>
    {
        private readonly IQueryProcessor _processor;

        public GetAllPlayablesQueryHandler(IQueryProcessor processor)
        {
            this._processor = processor;
        }

        public IQueryable<IPlayable> Handle(GetAllPlayablesQuery query)
        {
            var res = new List<IPlayable>();
            res.AddRange(_processor.Process(new GetPlayersQuery()));
            res.AddRange(_processor.Process(new GetPlayListsQuery()));
            res.AddRange(_processor.Process(new GetPlayableFilesQuery()));
            return res.AsQueryable();
        }
    }
}