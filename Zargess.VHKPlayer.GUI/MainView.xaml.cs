using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Zargess.VHKPlayer.GUI {
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView {
        private CommandPrompt Cmd { get; set; }
        private MainViewModel Vm { get; set; }

        public MainView() {
            InitializeComponent();
            Vm = new MainViewModel();
            Visibility = Visibility.Hidden;
            Cmd = new CommandPrompt(Vm) { Visibility = Visibility.Visible };
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e) {
            Cmd.Visibility = Visibility.Visible;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e) {
            base.OnClosing(e);
            Application.Current.Shutdown(0);
        }
    }
}