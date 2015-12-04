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
        private readonly ICommandProcessor _cprocessor;
        private readonly IQueryProcessor _qprocessor;
        private readonly IDataMonitor _monitor;

        public ObservableCollection<IPlayable> Playables { get; private set; } // TODO : Is no longer needed
        public ObservableCollection<ITab> Tabs { get; private set; }

        public IVideoPlayerController Controller { get; set; }
        public System.Windows.Input.ICommand PlayCommand { get; set; }


        public PlayerViewModel()
        {
            var container = App.Container;
            _cprocessor = container.Resolve<ICommandProcessor>();
            _qprocessor = container.Resolve<IQueryProcessor>();

            Playables = new ObservableCollection<IPlayable>();

            Controller = container.Resolve<IVideoPlayerController>();
            PlayCommand = new RunPlayableStrategyCommand(Controller);

            _monitor = container.Resolve<IDataMonitor>();

            _cprocessor.ProcessTransaction(new AddDataObserverCommand()
            {
                Observer = this
            });

            InitialiseData();

            Tabs = new ObservableCollection<ITab>();
        }

        public void InitialiseData()
        {
            _cprocessor.ProcessTransaction(new CreateAllPlayablesCommand());
        }

        // TODO : Optimize this such that it is not every tab that is updated everytime
        public void DataUpdated()
        {
            Playables.Clear();
            Tabs.Clear();
            Playables.AddAll(_qprocessor.Process(new GetAllPlayablesQuery()));
            Tabs.AddAll(_qprocessor.Process(new GetTabsFromStringSettingQuery() // TODO : Consider having the tabs in DataCenter and fetch them from there
            {
                SettingName = Constants.RightBlockTabsSettingName,
                Playables = Playables
            }));
        }
    }
}
