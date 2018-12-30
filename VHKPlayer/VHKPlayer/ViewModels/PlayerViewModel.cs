using System.ComponentModel;
using System.Windows;
using Autofac;
using VHKPlayer.Commands.GUI;
using VHKPlayer.Commands.Logic.AddApplicationObserver;
using VHKPlayer.Commands.Logic.CreateAllPlayables;
using VHKPlayer.Commands.Logic.CreateAllTabs;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Commands.Logic.ReloadTabs;
using VHKPlayer.Controllers.Interfaces;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Monitors.Interfaces;
using VHKPlayer.Queries.GetWindowPosition;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility;
using VHKPlayer.ViewModels.Interfaces;
using ICommand = System.Windows.Input.ICommand;

namespace VHKPlayer.ViewModels
{
    public class PlayerViewModel : IApplicationObserver, IMediaViewerViewModel
    {
        private readonly ICommandProcessor _processor;
        private readonly IDataMonitor _monitor;

        private ITabContainer TabContainer { get; }

        public IVideoPlayerController Controller { get; set; }
        private ICommand PlayCommand { get; set; }
        private ICommand BrowseForRootFolderCommand { get; set; }
        private ICommand BrowseForStatFolderCommand { get; set; }
        private ICommand ToggleAutoPlayListCommand { get; set; }
        private ICommand PlayAutoPlayListCommand { get; set; }
        public WindowPosition WindowPosition { get; set; }

        #region Move to settings

        private bool _statsEnabled;

        public bool StatsEnabled
        {
            get => _statsEnabled;

            set
            {
                _statsEnabled = value;
                RaiseEvent(nameof(StatsEnabled));
            }
        }

        private bool _soundEnabled;

        public bool SoundEnabled
        {
            get => _soundEnabled;

            set
            {
                _soundEnabled = value;
                RaiseEvent(nameof(SoundEnabled));
            }
        }

        private Thickness _savingPlacement = new Thickness(0.0, 0.0, 0.0, 0.0);

        public Thickness SavingPlacement
        {
            get => _savingPlacement;

            set
            {
                _savingPlacement = value;
                RaiseEvent(nameof(SavingPlacement));
            }
        }

        private Thickness _scoringPlacement = new Thickness(0.0, 0.0, 0.0, 0.0);

        public Thickness ScoringPlacement
        {
            get => _scoringPlacement;

            set
            {
                _scoringPlacement = value;
                RaiseEvent(nameof(ScoringPlacement));
            }
        }

        private Thickness _penaltyPlacement = new Thickness(0.0, 0.0, 0.0, 0.0);

        public Thickness PenaltyPlacement
        {
            get => _penaltyPlacement;

            set
            {
                _penaltyPlacement = value;
                RaiseEvent(nameof(PenaltyPlacement));
            }
        }

        private Thickness _textBlockPlacement = new Thickness(0.0, 0.0, 0.0, 0.0);

        public Thickness TextBlockPlacement
        {
            get => _textBlockPlacement;

            set
            {
                _textBlockPlacement = value;
                RaiseEvent(nameof(TextBlockPlacement));
            }
        }

        private double _textSize;

        public double TextSize
        {
            get => _textSize;

            set
            {
                _textSize = value;
                RaiseEvent(nameof(TextSize));
            }
        }

        private bool _fullscreen;

        public bool FullScreen
        {
            get => _fullscreen;

            set
            {
                _fullscreen = value;
                RaiseEvent(nameof(FullScreen));
            }
        }

        private int _screen;

        public int Screen
        {
            get => _screen;

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
        }

        private void InitialiseData()
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}