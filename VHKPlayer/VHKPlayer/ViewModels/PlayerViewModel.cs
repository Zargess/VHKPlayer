using Autofac;
using System.Collections.ObjectModel;
using System.Linq;
using VHKPlayer.Commands.GUI;
using VHKPlayer.Commands.Logic.AddApplicationObserver;
using VHKPlayer.Commands.Logic.AddDataObserver;
using VHKPlayer.Commands.Logic.CreateAllPlayables;
using VHKPlayer.Commands.Logic.CreateAllTabs;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Controllers;
using VHKPlayer.Controllers.Interfaces;
using VHKPlayer.Infrastructure;
using VHKPlayer.Infrastructure.Modules;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Monitors.Interfaces;
using VHKPlayer.Queries.GetAllPlayables;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility;

namespace VHKPlayer.ViewModels
{
    public class PlayerViewModel : ITabContainer, IApplicationObserver
    {
        private readonly ICommandProcessor _cprocessor;
        private readonly IQueryProcessor _qprocessor;
        private readonly IDataMonitor _dataMonitor;
        
        public ObservableCollection<ITab> Tabs { get; private set; }

        public IVideoPlayerController Controller { get; set; }
        public System.Windows.Input.ICommand PlayCommand { get; set; }


        public PlayerViewModel()
        {
            var container = App.Container;
            _cprocessor = container.Resolve<ICommandProcessor>();
            _qprocessor = container.Resolve<IQueryProcessor>();

            Controller = container.Resolve<IVideoPlayerController>();
            PlayCommand = new RunPlayableStrategyCommand(Controller);

            _dataMonitor = container.Resolve<IDataMonitor>();

            _cprocessor.Process(new AddApplicationObserverCommand
            {
                Observer = this
            });

            InitialiseData();

            Tabs = new ObservableCollection<ITab>();
            // TODO : Fix infinite loop. Created by the PlayerViewModel not being constant so an new one is created for each CreateTabCommand
            _cprocessor.Process(new CreateAllTabsCommand());
        }

        public void InitialiseData()
        {
            _cprocessor.ProcessTransaction(new CreateAllPlayablesCommand());
        }

        public void AddTab(ITab tab)
        {
            // TODO : Change this
            Tabs.Add(tab);
        }

        public void RemoveTab(ITab tab)
        {
            Tabs.Remove(tab);
        }

        public void ApplicationChanged(string settingName)
        {
            if (settingName != Constants.TabsSettingName) return;
            Tabs.Clear();
            _cprocessor.Process(new CreateAllTabsCommand());
        }
    }
}
