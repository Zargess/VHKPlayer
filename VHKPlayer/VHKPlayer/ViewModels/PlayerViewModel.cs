using Autofac;
using System.Collections.ObjectModel;
using System.Linq;
using VHKPlayer.Commands.GUI;
using VHKPlayer.Commands.Logic.AddDataObserver;
using VHKPlayer.Commands.Logic.CreateAllPlayables;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Controllers;
using VHKPlayer.Controllers.Interfaces;
using VHKPlayer.DataManagement.Interfaces;
using VHKPlayer.Infrastructure;
using VHKPlayer.Infrastructure.Modules;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Queries.GetAllPlayables;
using VHKPlayer.Queries.GetTabsFromStringSetting;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility;

namespace VHKPlayer.ViewModels
{
    public class PlayerViewModel : IDataObserver
    {
        private IContainer _container;
        private ICommandProcessor _cprocessor;
        private IQueryProcessor _qprocessor;
        private readonly IDataMonitor _monitor;

        public ObservableCollection<IPlayable> Playables { get; private set; }
        public ObservableCollection<ITab> Tabs { get; private set; }
        public IScript Script { get; set; } = new Script("(property name:Name value:\"Ladioo - 40 sek.mp3\")");

        public IVideoPlayerController Controller { get; set; }
        public System.Windows.Input.ICommand PlayCommand { get; set; }


        public PlayerViewModel()
        {
            _container = App.Container;
            _cprocessor = _container.Resolve<ICommandProcessor>();
            _qprocessor = _container.Resolve<IQueryProcessor>();

            Playables = new ObservableCollection<IPlayable>();

            var tabs = _qprocessor.Process(new GetTabsFromStringSettingQuery()
            {
                SettingName = Constants.RightBlockTabsSettingName
            });

            Tabs = new ObservableCollection<ITab>(tabs);

            Controller = _container.Resolve<IVideoPlayerController>();
            PlayCommand = new RunPlayableStrategyCommand(Controller);

            _monitor = _container.Resolve<IDataMonitor>();

            _cprocessor.ProcessTransaction(new AddDataObserverCommand()
            {
                Observer = this
            });
        }

        public void InitialiseData()
        {
            _cprocessor.ProcessTransaction(new CreateAllPlayablesCommand());
        }

        public void DataUpdated()
        {
            Playables.Clear();
            Playables.AddAll(_qprocessor.Process(new GetAllPlayablesQuery()));
        }
    }
}
