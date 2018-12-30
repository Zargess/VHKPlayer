using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Autofac;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Commands.Logic.StoreWindowPosition;
using VHKPlayer.Controllers;
using VHKPlayer.Controllers.Interfaces;
using VHKPlayer.Models;
using VHKPlayer.ViewModels;
using VHKPlayer.ViewModels.Interfaces;

namespace VHKPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PlayerViewModel _mainViewModel;
        private ISettingsViewModel _setViewModel;
        public IPlayController Controller { get; set; }

        private MediaViewer _viewer;
        private SettingsOverview _set;

        public MainWindow()
        {
            _mainViewModel = new PlayerViewModel();
            _setViewModel = new SettingsViewModel();
            _viewer = new MediaViewer(_setViewModel, _mainViewModel.Controller);
            _set = new SettingsOverview(_setViewModel);
            _set.Visibility = Visibility.Hidden;
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.Closing += MainWindow_Closing;
            this.KeyUp += MainWindow_KeyUp;
            App.Dispatch = this.Dispatcher;
            _viewer.Visibility = Visibility.Visible;
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            // TODO : Make a lookup in a command library of some sort contained inside the viewmodel and changeable inside settings

            if (e.Key == Key.F11 || e.SystemKey == Key.F11)
            {
                if (Controller.AudioState == MediaPlayerState.Playing)
                {
                    Controller.Pause(FileType.Audio);
                }
                else
                {
                    Controller.Resume(FileType.Audio);
                }
            }
            else if (e.Key == Key.F10 || e.SystemKey == Key.F10)
            {
                if (Controller.VideoState == MediaPlayerState.Playing)
                {
                    Controller.Pause(FileType.Video);
                }
                else
                {
                    Controller.Resume(FileType.Video);
                }
            }
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            _mainViewModel.Controller.Shutdown();
            _viewer.ShouldClose = true;
            _viewer.Close();
            _set.ShouldClose = true;
            _set.Close();

            App.Container.Resolve<ICommandProcessor>().Process(new StoreWindowPositionCommand
            {
                Position = new WindowPosition
                {
                    Top = this.Top,
                    Left = this.Left,
                    Height = this.Height,
                    Width = this.Width,
                    Maximized = this.WindowState == WindowState.Maximized
                }
            });

            Environment.Exit(0);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = _mainViewModel;
            View.Video.MediaEnded += (s, ee) => _mainViewModel.Controller.PlayQueue();
            Controller = new PlayController(_viewer.View);
            _mainViewModel.Controller.AddObserver(Controller);

            if (_mainViewModel.WindowPosition.Height == 0 || _mainViewModel.WindowPosition.Width == 0) return;

            this.Top = _mainViewModel.WindowPosition.Top;
            this.Left = _mainViewModel.WindowPosition.Left;
            this.Height = _mainViewModel.WindowPosition.Height;
            this.Width = _mainViewModel.WindowPosition.Width;

            if (_mainViewModel.WindowPosition.Maximized)
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        private void Show_Settings(object sender, RoutedEventArgs e)
        {
            _set.Visibility = Visibility.Visible;
        }
    }
}