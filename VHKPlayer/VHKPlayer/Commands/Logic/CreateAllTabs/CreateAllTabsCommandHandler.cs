using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.CreateTab;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Infrastructure;
using VHKPlayer.Models;
using VHKPlayer.Queries.GetPlayStrategy;
using VHKPlayer.Queries.GetStringSetting;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility;

namespace VHKPlayer.Commands.Logic.CreateAllTabs
{
    class CreateAllTabsCommandHandler : ICommandHandler<CreateAllTabsCommand>
    {
        private readonly ICommandProcessor _cprocessor;
        private readonly IQueryProcessor _qprocessor;

        public CreateAllTabsCommandHandler(ICommandProcessor cprocessor, IQueryProcessor qprocessor)
        {
            _cprocessor = cprocessor;
            _qprocessor = qprocessor;
        }

        public void Handle(CreateAllTabsCommand command)
        {
            var defs = _qprocessor.Process(new GetStringSettingQuery()
            {
                SettingName = Constants.TabsSettingName
            })
            .Split(',')
            .Select(def => def.Replace("{", "").Replace("}", "").Split(';'));

            foreach (var arguments in defs)
            {
                _cprocessor.Process(new CreateTabCommand()
                {
                    Name = arguments[0],
                    Placement = (TabPlacement)Enum.Parse(typeof(TabPlacement), arguments[1]),
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
