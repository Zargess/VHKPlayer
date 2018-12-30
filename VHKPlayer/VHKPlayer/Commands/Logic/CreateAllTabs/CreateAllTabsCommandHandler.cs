using System;
using VHKPlayer.Commands.Logic.CreateTab;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Infrastructure;
using VHKPlayer.Models;
using VHKPlayer.Queries.GetPlayStrategy;
using VHKPlayer.Queries.GetStringSetting;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility;
using VHKPlayer.Utility.GetSpecialTab.Interfaces;

namespace VHKPlayer.Commands.Logic.CreateAllTabs
{
    class CreateAllTabsCommandHandler : ICommandHandler<CreateAllTabsCommand>
    {
        private readonly ICommandProcessor _cprocessor;
        private readonly IQueryProcessor _qprocessor;
        private readonly IGetSpecialTabStrategy _strategy;

        public CreateAllTabsCommandHandler(ICommandProcessor cprocessor, IGetSpecialTabStrategy strategy,
            IQueryProcessor qprocessor)
        {
            _cprocessor = cprocessor;
            _strategy = strategy;
            _qprocessor = qprocessor;
        }

        public void Handle(CreateAllTabsCommand command)
        {
            var defs = _qprocessor.Process(new GetStringSettingQuery()
                {
                    SettingName = Constants.TabsSettingName
                })
                .Split(',');

            foreach (var def in defs)
            {
                if (_strategy.IsSpecialTab(def))
                {
                    _cprocessor.Process(new CreateTabCommand()
                    {
                        Name = def
                    });
                }
                else
                {
                    var arguments = def.Replace("{", "").Replace("}", "").Split(';');
                    _cprocessor.Process(new CreateTabCommand()
                    {
                        Name = arguments[0],
                        Placement = (TabPlacement) Enum.Parse(typeof(TabPlacement), arguments[1]),
                        Number = arguments[2].ToInteger(),
                        Script = new Script(arguments[3]),
                        PlayListTab = arguments[4].ToBool(),
                        PlayStrategy = _qprocessor.Process(new GetPlayStrategyQuery()
                        {
                            StrategyName = arguments[5]
                        })
                    });
                }
            }
        }
    }
}