using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VHKPlayer.ViewModels.Interfaces;

namespace VHKPlayer
{
    /// <summary>
    /// Interaction logic for MediaViewer.xaml
    /// </summary>
    public partial class MediaViewer : Window
    {
        private readonly ISettingsViewModel _viewmodel;

        public bool ShouldClose { get; set; }

        public MediaViewer(ISettingsViewModel viewmodel)
        {
            InitializeComponent();
            ShouldClose = false;
            this.Closing += MediaViewer_Closing;
            this.Loaded += MediaViewer_Loaded;
            _viewmodel = viewmodel;
            _viewmodel.PropertyChanged += SettingsChanged;
        }

        private void SettingsChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            UpdateScreenProperties();
            UpdateFullscreenMode();
        }

        private void UpdateFullscreenMode()
        {
            throw new NotImplementedException();
        }

        private void UpdateScreenProperties()
        {
            var screen = GetScreen();

            if (!this.IsLoaded)
                this.WindowStartupLocation = WindowStartupLocation.Manual;

            var workingArea = screen.WorkingArea;
            this.Left = workingArea.Left;
            this.Top = workingArea.Top;
            this.Width = workingArea.Width;
            this.Height = workingArea.Height;
            // If window isn't loaded then maxmizing will result in the window displaying on the primary monitor
            if (this.IsLoaded)
                this.WindowState = WindowState.Maximized;

        }

        private Screen GetScreen()
        {
            if (_viewmodel.Screen <= 0) return Screen.PrimaryScreen;

            var secondaryScreens = Screen.AllScreens.Where(s => !s.Primary).ToList();
            if (secondaryScreens.Count == 0) return Screen.PrimaryScreen;

            if (_viewmodel.Screen < secondaryScreens.Count) return secondaryScreens[_viewmodel.Screen];

            return secondaryScreens[secondaryScreens.Count - 1];
        }

        private void MediaViewer_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = _viewmodel;
        }

        private void MediaViewer_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !ShouldClose;
        }
    }
}
