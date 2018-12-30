using System;
using System.Windows;
using System.Windows.Input;
using VHKPlayer.Controllers;
using VHKPlayer.Controllers.Interfaces;
using VHKPlayer.Models;
using VHKPlayer.ViewModels;

namespace VHKPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PlayerViewModel _mainViewModel;
        private SettingsViewModel _setViewModel;
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

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _mainViewModel.Controller.Shutdown();
            _viewer.ShouldClose = true;
            _viewer.Close();
            _set.ShouldClose = true;
            _set.Close();
            Environment.Exit(0);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = _mainViewModel;
            View.Video.MediaEnded += (s, ee) => _mainViewModel.Controller.PlayQueue();
            Controller = new PlayController(_viewer.View);
            _mainViewModel.Controller.AddObserver(Controller);
        }

        private void Show_Settings(object sender, RoutedEventArgs e)
        {
            _set.Visibility = Visibility.Visible;
        }
    }
}