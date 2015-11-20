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
        private readonly IGlobalConfigService config;
        private readonly IQueryProcessor processor;
        private readonly IGetSpecialTabStrategy strategy;

        public GetTabsFromStringSettingQueryHandler(IGlobalConfigService config, IQueryProcessor processor, IGetSpecialTabStrategy strategy)
        {
            this.config = config;
            this.processor = processor;
            this.strategy = strategy;
        }

        public IQueryable<ITab> Handle(GetTabsFromStringSettingQuery query)
        {
            var res = new List<ITab>();

            var setting = config.GetString(query.SettingName);
            var definitions = setting.Split(',');

            foreach (var def in definitions)
            {
                if (strategy.IsSpecialTab(def))
                {
                    res.Add(strategy.CreateSpecialTab(def));
                }
                else
                {
                    var variables = def.Replace("{", "").Replace("}", "").Split(';');
                    var tab = new PlayableContentTab()
                    {
                        Name = variables[0],
                        Script = new Script(variables[2]),
                        PlayListTab = variables[2].ToBool(),
                        PlayStrategy = processor.Process(new GetPlayStrategyQuery()
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
