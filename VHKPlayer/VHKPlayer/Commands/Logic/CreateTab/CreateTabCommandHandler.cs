using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Interpreter.Interfaces;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Queries.GetAllPlayables;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility.GetSpecialTab.Interfaces;

namespace VHKPlayer.Commands.Logic.CreateTab
{
    public class CreateTabCommandHandler : ICommandHandler<CreateTabCommand>
    {
        private readonly ITabContainer _container;
        private readonly IScriptInterpreter _interpreter;
        private readonly IQueryProcessor _processor;
        private readonly IGetSpecialTabStrategy _strategy;

        public CreateTabCommandHandler(ITabContainer container, IScriptInterpreter interpreter, IQueryProcessor processor, IGetSpecialTabStrategy strategy)
        {
            _container = container;
            _interpreter = interpreter;
            _processor = processor;
            _strategy = strategy;
        }

        public void Handle(CreateTabCommand command)
        {
            if (_strategy.IsSpecialTab(command.Name))
            {
                _container.Tab.Add(_strategy.CreateSpecialTab(command.Name));
            }
            else
            {
                var playables = _processor.Process(new GetAllPlayablesQuery());
                var data = new ObservableCollection<IPlayable>(playables.AsParallel().Where(x => _interpreter.Evaluate(command.Script, x)));
                _container.Tab.Add(new PlayableContentTab
                {
                    Name = command.Name,
                    Number = command.Number,
                    Data = data,
                    Placement = command.Placement,
                    PlayListTab = command.PlayListTab,
                    PlayStrategy = command.PlayStrategy
                });
            }
        }
    }
}
