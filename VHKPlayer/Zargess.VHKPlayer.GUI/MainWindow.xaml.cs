using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Zargess.VHKPlayer.FileManagement;
using Zargess.VHKPlayer.FileManagement.Interfaces;
using Zargess.VHKPlayer.GUI.ViewModels;
using Zargess.VHKPlayer.SettingsManager;

namespace Zargess.VHKPlayer.GUI {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private MainViewModel ViewModel { get; set; }
        public MainWindow() {
            var root = @"D:\Dropbox\Programmering\C#\damer 2013-2014";
            if (Directory.Exists(root)) SettingsManagement.Instance.SetSetting("root", root);
            else SettingsManagement.Instance.SetSetting("root", @"C:\Users\MFH\vhk");
            InitializeComponent();
            ViewModel = new MainViewModel(new VHKPlayerFactory());
            DataContext = ViewModel;
        }

        public void ListBoxItem_Click(object sender, MouseButtonEventArgs e) {
            var item = sender as StackPanel;
            if (item == null) return;
            var element = item.Tag as IPlayable;
            if (element == null) return;
            Console.WriteLine(element.Name);
        }
    }
}