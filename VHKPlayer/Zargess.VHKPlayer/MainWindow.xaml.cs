using MahApps.Metro.Controls;
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
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.EventHandlers;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.Observers;
using Zargess.VHKPlayer.Utility;

namespace Zargess.VHKPlayer {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow {

        public MainWindow() {
            var path = @"D:\Dropbox\Programmering\C#";
            var path2 = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Dropbox\Programmering\C#";
            var root = "damer 2013-2014";
            var statsFolder = "digimatch";
            Console.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
            if (Directory.Exists(path)) {
                App.ConfigService.Update("root", PathHandler.CombinePaths(path, root));
                App.ConfigService.Update("statsFolder", PathHandler.CombinePaths(path, statsFolder));
            } else {
                App.ConfigService.Update("root", PathHandler.CombinePaths(path2, root));
                App.ConfigService.Update("statsFolder", PathHandler.CombinePaths(path2, statsFolder));
            }

            InitializeComponent();
            DataContext = App.PlayerViewModel;
            App.PlayManager.AddObserver(new PlayObserver(Viewer, Audio, false));
        }
    }
}