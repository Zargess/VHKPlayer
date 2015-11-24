using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models;
using VHKPlayer.Queries.GetPlayListByName;
using VHKPlayer.Queries.GetStringSetting;
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
            var name = _processor.Process(new GetStringSettingQuery()
            {
                SettingName = Constants.AutoPlayListSettingName
            });
            return _processor.Process(new GetPlayListByNameQuery()
            {
                Name = name
            });
        }
    }
}
