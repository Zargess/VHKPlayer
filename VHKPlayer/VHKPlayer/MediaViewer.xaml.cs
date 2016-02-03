using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
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
        private readonly IMediaViewerViewModel _viewmodel;

        public bool ShouldClose { get; set; }

        public MediaViewer(IMediaViewerViewModel viewmodel)
        {
            InitializeComponent();
            ShouldClose = false;
            this.Closing += MediaViewer_Closing;
            this.Loaded += MediaViewer_Loaded;
            _viewmodel = viewmodel;
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
