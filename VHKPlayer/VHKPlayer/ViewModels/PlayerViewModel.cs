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
using VHKPlayer.ViewModels.Interfaces;
using System;
using System.Windows;
using VHKPlayer.Queries.GetWindowPosition;

namespace VHKPlayer.ViewModels
{
    public class PlayerViewModel : IApplicationObserver, IMediaViewerViewModel
    {
        private readonly ICommandProcessor _processor;
        private readonly IDataMonitor _monitor;
        
        public ITabContainer TabContainer { get; }

        public IVideoPlayerController Controller { get; set; }
        public System.Windows.Input.ICommand PlayCommand { get; set; }
        public System.Windows.Input.ICommand BrowseForRootFolderCommand { get; set; }
        public System.Windows.Input.ICommand BrowseForStatFolderCommand { get; set; }
        public System.Windows.Input.ICommand ToggleAutoPlayListCommand { get; set; }
        public System.Windows.Input.ICommand PlayAutoPlayListCommand { get; set; }
        public WindowPosition WindowPosition { get; set; }

        #region Move to settings
        private bool _statsEnabled;
        public bool StatsEnabled
        {
            get
            {
                return _statsEnabled;
            }

            set
            {
                _statsEnabled = value;
                RaiseEvent(nameof(StatsEnabled));
            }
        }

        private bool _soundEnabled;
        public bool SoundEnabled
        {
            get
            {
                return _soundEnabled;
            }

            set
            {
                _soundEnabled = value;
                RaiseEvent(nameof(SoundEnabled));
            }
        }

        private Thickness _savingPlacement = new Thickness(0.0, 0.0, 0.0, 0.0);
        public Thickness SavingPlacement
        {
            get
            {
                return _savingPlacement;
            }

            set
            {
                _savingPlacement = value;
                RaiseEvent(nameof(SavingPlacement));
            }
        }

        private Thickness _scoringPlacement = new Thickness(0.0, 0.0, 0.0, 0.0);
        public Thickness ScoringPlacement
        {
            get
            {
                return _scoringPlacement;
            }

            set
            {
                _scoringPlacement = value;
                RaiseEvent(nameof(ScoringPlacement));
            }
        }

        private Thickness _penaltyPlacement = new Thickness(0.0, 0.0, 0.0, 0.0);
        public Thickness PenaltyPlacement
        {
            get
            {
                return _penaltyPlacement;
            }

            set
            {
                _penaltyPlacement = value;
                RaiseEvent(nameof(PenaltyPlacement));
            }
        }

        private Thickness _textBlockPlacement = new Thickness(0.0, 0.0, 0.0, 0.0);
        public Thickness TextBlockPlacement
        {
            get
            {
                return _textBlockPlacement;
            }

            set
            {
                _textBlockPlacement = value;
                RaiseEvent(nameof(TextBlockPlacement));
            }
        }

        private double _textSize;
        public double TextSize
        {
            get
            {
                return _textSize;
            }

            set
            {
                _textSize = value;
                RaiseEvent(nameof(TextSize));
            }
        }

        private bool _fullscreen;
        public bool FullScreen
        {
            get
            {
                return _fullscreen;
            }

            set
            {
                _fullscreen = value;
                RaiseEvent(nameof(FullScreen));
            }
        }

        private int _screen;
        public int Screen
        {
            get
            {
                return _screen;
            }

            set
            {
                _screen = value;
                RaiseEvent(nameof(Screen));
            }
        }
        #endregion
        public PlayerViewModel()
        {
            var container = App.Container;
            _processor = container.Resolve<ICommandProcessor>();
            _monitor = container.Resolve<IDataMonitor>();
            Controller = container.Resolve<IVideoPlayerController>();
            PlayCommand = new RunPlayableStrategyCommand(Controller);
            BrowseForRootFolderCommand = new BrowseForRootFolderCommand();
            BrowseForStatFolderCommand = new BrowseForStatFolderCommand();
            ToggleAutoPlayListCommand = new AutoPlayListCommand(Controller);
            PlayAutoPlayListCommand = new PlayAutoPlayListCommand(container.Resolve<IQueryProcessor>(), Controller);

            _processor.Process(new AddApplicationObserverCommand
            {
                Observer = this
            });

            // TODO : Initialise all settings
            WindowPosition = container.Resolve<IQueryProcessor>().Process(new GetWindowPositionQuery());

            TabContainer = container.Resolve<ITabContainer>();
            InitialiseData();
            _processor.Process(new CreateAllTabsCommand());
            var test = container.Resolve<IQueryProcessor>().Process(new GetAllPlayablesQuery()).Where(x => x.GetType() == typeof(PlayList)).ToList();
        }

        public void InitialiseData()
        {
            _processor.ProcessTransaction(new CreateAllPlayablesCommand());
        }
        
        public void ApplicationChanged(string settingName)
        {
            if (settingName == Constants.TabsSettingName)
            {
                _processor.Process(new ReloadTabsCommand());
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaiseEvent(string name)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
