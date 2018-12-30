using System.Linq;
using VHKPlayer.Models;
using VHKPlayer.Queries.GetPlayLists;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetPlayListByName
{
    class GetPlayListByNameQueryHandler : IQueryHandler<GetPlayListByNameQuery, PlayList>
    {
        private readonly IQueryProcessor _processor;

        public GetPlayListByNameQueryHandler(IQueryProcessor processor)
        {
            this._processor = processor;
        }

        public PlayList Handle(GetPlayListByNameQuery query)
        {
            var playlists = _processor.Process(new GetPlayListsQuery());
            if (!playlists.Any(x => x.Name.ToLower() == query.Name.ToLower())) return null;
            return playlists.Single(x => x.Name.ToLower() == query.Name.ToLower());
        }
    }
}