using Autofac;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using VHKPlayer.Commands.GUI;
using VHKPlayer.Commands.Logic.AddApplicationObserver;
using VHKPlayer.Commands.Logic.AddDataObserver;
using VHKPlayer.Commands.Logic.CreateAllPlayables;
using VHKPlayer.Commands.Logic.CreateAllTabs;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Commands.Logic.ReloadTabs;
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
    public class PlayerViewModel : IApplicationObserver, INotifyPropertyChanged
    {
        private readonly ICommandProcessor _cprocessor;
        
        public ITabContainer TabContainer { get; }

        public IVideoPlayerController Controller { get; set; }
        public System.Windows.Input.ICommand PlayCommand { get; set; }

        


        public PlayerViewModel()
        {
            var container = App.Container;
            _cprocessor = container.Resolve<ICommandProcessor>();

            Controller = container.Resolve<IVideoPlayerController>();
            PlayCommand = new RunPlayableStrategyCommand(Controller);

            _cprocessor.Process(new AddApplicationObserverCommand
            {
                Observer = this
            });

            TabContainer = container.Resolve<ITabContainer>();
            _cprocessor.Process(new CreateAllTabsCommand());

            InitialiseData();
        }

        public void InitialiseData()
        {
            _cprocessor.ProcessTransaction(new CreateAllPlayablesCommand());
        }
        
        public void ApplicationChanged(string settingName)
        {
            if (settingName != Constants.TabsSettingName) return;
            _cprocessor.Process(new ReloadTabsCommand());
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
