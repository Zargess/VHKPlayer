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
using Zargess.VHKPlayer.ViewModels;

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
            Visibility = Visibility.Visible;
            DataContext = Vm;
            Cmd = new CommandPrompt(Vm) { Visibility = Visibility.Hidden };
        }

        public void RunCommand(string command) {
            Cmd.RunCommand(command);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e) {
            Cmd.Visibility = Visibility.Hidden;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e) {
            base.OnClosing(e);
            Application.Current.Shutdown(0);
        }

        private void MenuItemClick(object sender, RoutedEventArgs e) {
            var item = sender as MenuItem;
            if (item == null) return;
            if (item.Tag.ToString() == "showConsole" && Cmd.Visibility == Visibility.Hidden) {
                Cmd.Visibility = Visibility.Visible;
                item.Header = "Hide Console";
                return;
            } else if (item.Tag.ToString() == "showConsole") {
                Cmd.Visibility = Visibility.Hidden;
                item.Header = "Show Console";
                return;
            }
            RunCommand(item.Tag.ToString());
        }

        private void ListBoxItem_Click(object sender, MouseButtonEventArgs e) {
            var item = sender as StackPanel;
            if (item == null) return;
            RunCommand("play " + item.Name + " " + item.Tag);
        }
    }
}