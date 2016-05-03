using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Exceptions;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility.Settings.Interfaces;

namespace VHKPlayer.Queries.GetBoolSetting
{
    class GetBoolSettingQueryHandler : IQueryHandler<GetBoolSettingQuery, bool>
    {
        private readonly IGlobalConfigService _config;

        public GetBoolSettingQueryHandler(IGlobalConfigService config)
        {
            _config = config;
        }

        public bool Handle(GetBoolSettingQuery query)
        {
            var setting = _config.GetObject(query.SettingName);

            if (setting is bool) return (bool)setting;

            throw new SettingIsNotOfExpectedTypeException("The setting " + query.SettingName + " is not a double setting.");
        }
    }
}
