using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Exceptions;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility.Settings.Interfaces;

namespace VHKPlayer.Queries.GetDoubleSetting
{
    class GetDoubleSettingQueryHandler : IQueryHandler<GetDoubleSettingQuery, double>
    {
        private readonly IGlobalConfigService _config;

        public GetDoubleSettingQueryHandler(IGlobalConfigService config)
        {
            _config = config;
        }

        public double Handle(GetDoubleSettingQuery query)
        {
            var setting = _config.GetObject(query.SettingName);

            if (setting is double) return (double)setting;

            throw new SettingIsNotOfExpectedTypeException("The setting " + query.SettingName + " is not a double setting.");
        }
    }
}
