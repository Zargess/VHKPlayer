using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models;
using VHKPlayer.Queries.GetPlayLists;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetPlayListByName
{
    class GetPlayListByNameQueryHandler : IQueryHandler<GetPlayListByNameQuery, PlayList>
    {
        private readonly IQueryProcessor processor;

        public GetPlayListByNameQueryHandler(IQueryProcessor processor)
        {
            this.processor = processor;
        }

        public PlayList Handle(GetPlayListByNameQuery query)
        {
            var playlists = processor.Process(new GetPlayListsQuery());
            if (!playlists.Any(x => x.Name.ToLower() == query.Name.ToLower())) return null;
            return playlists.Single(x => x.Name.ToLower() == query.Name.ToLower());
        }
    }
}
