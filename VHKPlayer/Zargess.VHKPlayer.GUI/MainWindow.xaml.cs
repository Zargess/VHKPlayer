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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Zargess.VHKPlayer.FileManagement;
using Zargess.VHKPlayer.FileManagement.Interfaces;
using Zargess.VHKPlayer.GUI.ViewModels;

namespace Zargess.VHKPlayer.GUI {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private MainViewModel ViewModel { get; set; }
        public MainWindow() {
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