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

namespace VHKPlayer.Queries.GetGoalPlayList
{
    class GetGoalPlayListQueryHandler : IQueryHandler<GetGoalPlayListQuery, PlayList>
    {
        private readonly IQueryProcessor _processor;

        public GetGoalPlayListQueryHandler(IQueryProcessor processor)
        {
            _processor = processor;
        }

        public PlayList Handle(GetGoalPlayListQuery query)
        {
            var name = _processor.Process(new GetStringSettingQuery()
            {
                SettingName = Constants.GoalPlayList
            });
            return _processor.Process(new GetPlayListByNameQuery()
            {
                Name = name
            });
        }
    }
}
