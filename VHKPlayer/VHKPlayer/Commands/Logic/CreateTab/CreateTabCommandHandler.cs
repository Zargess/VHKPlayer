using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using VHKPlayer.Commands.Logic.AddDataObserver;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Infrastructure;
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
        private readonly ICommandProcessor _cprocessor;
        private readonly IScriptInterpreter _interpreter;
        private readonly IQueryProcessor _qprocessor;
        private readonly IGetSpecialTabStrategy _strategy;

        public CreateTabCommandHandler(ITabContainer container, IScriptInterpreter interpreter, ICommandProcessor cprocessor, IQueryProcessor qprocessor, IGetSpecialTabStrategy strategy)
        {
            _container = container;
            _interpreter = interpreter;
            _cprocessor = cprocessor;
            _qprocessor = qprocessor;
            _strategy = strategy;
        }

        public void Handle(CreateTabCommand command)
        {
            var tab = ConstructTab(command);

            var collection = _container.GetCollectionFromPlacement(command.Placement);
            collection.Add(tab);
            collection.SetCollection(collection.OrderBy(x => x.Number));
        }

        private ITab ConstructTab(CreateTabCommand command)
        {
            if (_strategy.IsSpecialTab(command.Name))
            {
                return _strategy.CreateSpecialTab(command.Name);
            }
            else
            {
                var playables = _qprocessor.Process(new GetAllPlayablesQuery());
                var data = new ObservableCollection<IPlayable>(playables.Where(x => _interpreter.Evaluate(command.Script, x)));
                var tab = new PlayableContentTab(_interpreter, _qprocessor)
                {
                    Name = command.Name,
                    Number = command.Number,
                    Data = data,
                    Placement = command.Placement,
                    PlayListTab = command.PlayListTab,
                    PlayStrategy = command.PlayStrategy,
                    Script = command.Script
                };

                _cprocessor.Process(new AddDataObserverCommand()
                {
                    Observer = tab
                });

                return tab;
            }
        }
    }
}