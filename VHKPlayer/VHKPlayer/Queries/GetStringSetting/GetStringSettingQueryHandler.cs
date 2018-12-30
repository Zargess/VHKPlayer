using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility.Settings.Interfaces;

namespace VHKPlayer.Queries.GetStringSetting
{
    class GetStringSettingQueryHandler : IQueryHandler<GetStringSettingQuery, string>
    {
        private readonly IGlobalConfigService _configService;

        public GetStringSettingQueryHandler(IGlobalConfigService configService)
        {
            this._configService = configService;
        }

        public string Handle(GetStringSettingQuery query)
        {
            return _configService.GetString(query.SettingName);
        }
    }
}