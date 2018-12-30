using VHKPlayer.Models;
using VHKPlayer.Queries.GetPlayListByName;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility;

namespace VHKPlayer.Queries.GetAutoPlayList
{
    class GetAutoPlayListQueryHandler : IQueryHandler<GetAutoPlayListQuery, PlayList>
    {
        private readonly IQueryProcessor _processor;

        public GetAutoPlayListQueryHandler(IQueryProcessor processor)
        {
            this._processor = processor;
        }

        public PlayList Handle(GetAutoPlayListQuery query)
        {
            return _processor.Process(new GetPlayListByNameQuery()
            {
                Name = Constants.AutoPlayListName
            });
        }
    }
}