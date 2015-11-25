using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Exceptions;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility.Settings.Interfaces;
using VHKPlayer.Infrastructure;

namespace VHKPlayer.Queries.GetIntSetting
{
    public class GetIntSettingQueryHandler : IQueryHandler<GetIntSettingQuery, int>
    {
        private readonly IGlobalConfigService _config;

        public GetIntSettingQueryHandler(IGlobalConfigService config)
        {
            _config = config;
        }

        public int Handle(GetIntSettingQuery query)
        {
            var setting = _config.GetObject(query.SettingName);

            if (setting is int) return (int) setting;

            throw new SettingIsNotOfExpectedTypeException("The setting " + query.SettingName + " is not a integer setting.");
        }
    }
}
