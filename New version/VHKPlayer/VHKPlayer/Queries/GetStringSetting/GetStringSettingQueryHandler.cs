using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetStringSetting
{
    class GetStringSettingQueryHandler : IQueryHandler<GetStringSettingQuery, string>
    {
        public string Handle(GetStringSettingQuery query)
        {
            return App.Config.GetString(query.SettingName);
        }
    }
}
