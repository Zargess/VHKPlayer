﻿using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility.Settings.Interfaces;

namespace VHKPlayer.Queries.GetStringSetting
{
    class GetStringSettingQueryHandler : IQueryHandler<GetStringSettingQuery, string>
    {
        private readonly IGlobalConfigService configService;

        public GetStringSettingQueryHandler(IGlobalConfigService configService)
        {
            this.configService = configService;
        }

        public string Handle(GetStringSettingQuery query)
        {
            return configService.GetString(query.SettingName);
        }
    }
}
