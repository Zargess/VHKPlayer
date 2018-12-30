using System.Linq;
using System.Windows;
using System.Windows.Forms;
using VHKPlayer.Controllers.Interfaces;
using VHKPlayer.ViewModels.Interfaces;

namespace VHKPlayer
{
    /// <summary>
    /// Interaction logic for MediaViewer.xaml
    /// </summary>
    public partial class MediaViewer : Window
    {
        private readonly ISettingsViewModel _viewmodel;
        private double _originalWidth, _originalHeight;
        public bool ShouldClose { get; set; }

        public MediaViewer(ISettingsViewModel viewmodel, IVideoPlayerController controller)
        {
            InitializeComponent();
            ShouldClose = false;
            this.Closing += MediaViewer_Closing;
            this.Loaded += MediaViewer_Loaded;
            this.View.Video.MediaEnded += (sender, ee) => controller.PlayQueue();
            _viewmodel = viewmodel;
            _viewmodel.PropertyChanged += SettingsChanged;
        }

        private void SettingsChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            UpdateScreenSettings();
        }

        private void MediaViewer_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = _viewmodel;
            _originalHeight = this.Height;
            _originalWidth = this.Width;
            UpdateScreenSettings();
        }

        private void MediaViewer_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !ShouldClose;
        }

        private void UpdateScreenSettings()
        {
            var screen = GetScreen();
            UpdateScreenLocation(screen);
            UpdateFullscreenMode(screen);
        }

        private void UpdateFullscreenMode(Screen screen)
        {
            var workingArea = screen.WorkingArea;
            if (!_viewmodel.FullScreen)
            {
                this.Width = _originalWidth;
                this.Height = _originalHeight;
                this.WindowState = WindowState.Normal;
                this.WindowStyle = WindowStyle.SingleBorderWindow;
            }
            else
            {
                this.Width = workingArea.Width;
                this.Height = workingArea.Height;
                this.WindowStyle = WindowStyle.None;
                // If window isn't loaded then maxmizing will result in the window displaying on the primary monitor
                if (this.IsLoaded)
                    this.WindowState = WindowState.Maximized;
            }
        }

        private void UpdateScreenLocation(Screen screen)
        {
            if (!this.IsLoaded)
                this.WindowStartupLocation = WindowStartupLocation.Manual;

            var workingArea = screen.WorkingArea;
            this.Left = workingArea.Left;
            this.Top = workingArea.Top;
        }

        private Screen GetScreen()
        {
            if (_viewmodel.Screen <= 0) return Screen.PrimaryScreen;

            var secondaryScreens = Screen.AllScreens.Where(s => !s.Primary).ToList();
            if (secondaryScreens.Count == 0) return Screen.PrimaryScreen;

            if (_viewmodel.Screen - 1 < secondaryScreens.Count) return secondaryScreens[_viewmodel.Screen - 1];

            return secondaryScreens[secondaryScreens.Count - 1];
        }
    }
}