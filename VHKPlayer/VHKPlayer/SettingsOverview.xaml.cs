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
    /// Interaction logic for SettingsOverview.xaml
    /// </summary>
    public partial class SettingsOverview : Window
    {
        private readonly ISettingsViewModel _viewmodel;

        public bool ShouldClose { get; set; }

        public SettingsOverview(ISettingsViewModel viewmodel)
        {
            InitializeComponent();
            ShouldClose = false;
            _viewmodel = viewmodel;
            this.Closing += SettingsOverview_Closing;
            this.Loaded += SettingsOverview_Loaded;
        }

        private void SettingsOverview_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = _viewmodel;
        }

        private void SettingsOverview_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !ShouldClose;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
