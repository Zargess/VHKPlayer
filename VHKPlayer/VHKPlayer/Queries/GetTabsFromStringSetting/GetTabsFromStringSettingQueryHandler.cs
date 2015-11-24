using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Infrastructure;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Queries.GetPlayStrategy;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility.GetSpecialTab.Interfaces;
using VHKPlayer.Utility.Settings.Interfaces;

namespace VHKPlayer.Queries.GetTabsFromStringSetting
{
    class GetTabsFromStringSettingQueryHandler : IQueryHandler<GetTabsFromStringSettingQuery, IQueryable<ITab>>
    {
        private readonly IGlobalConfigService _config;
        private readonly IQueryProcessor _processor;
        private readonly IGetSpecialTabStrategy _strategy;

        public GetTabsFromStringSettingQueryHandler(IGlobalConfigService config, IQueryProcessor processor, IGetSpecialTabStrategy strategy)
        {
            this._config = config;
            this._processor = processor;
            this._strategy = strategy;
        }

        public IQueryable<ITab> Handle(GetTabsFromStringSettingQuery query)
        {
            var res = new List<ITab>();

            var setting = _config.GetString(query.SettingName);
            var definitions = setting.Split(',');

            foreach (var def in definitions)
            {
                if (_strategy.IsSpecialTab(def))
                {
                    res.Add(_strategy.CreateSpecialTab(def));
                }
                else
                {
                    var variables = def.Replace("{", "").Replace("}", "").Split(';');
                    var script = new Script(variables[1]);
                    var tab = new PlayableContentTab()
                    {
                        Name = variables[0],
                        Script = script,
                        PlayListTab = variables[2].ToBool(),
                        PlayStrategy = _processor.Process(new GetPlayStrategyQuery()
                        {
                            StrategyName = variables[3]
                        })
                    };

                    res.Add(tab);
                }
            }

            return res.AsQueryable();
        }
    }
}
