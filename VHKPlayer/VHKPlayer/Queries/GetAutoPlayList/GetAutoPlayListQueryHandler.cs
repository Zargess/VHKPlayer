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
        private readonly IQueryProcessor processor;

        public GetAutoPlayListQueryHandler(IQueryProcessor processor)
        {
            this.processor = processor;
        }

        public PlayList Handle(GetAutoPlayListQuery query)
        {
            var name = processor.Process(new GetStringSettingQuery()
            {
                SettingName = Constants.AutoPlayListSettingName
            });
            return processor.Process(new GetPlayListByNameQuery()
            {
                Name = name
            });
        }
    }
}
