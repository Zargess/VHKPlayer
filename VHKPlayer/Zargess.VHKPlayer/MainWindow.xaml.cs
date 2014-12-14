﻿using MahApps.Metro.Controls;
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

namespace Zargess.VHKPlayer {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow {
        public MainWindow() {
            var root = @"D:\Dropbox\Programmering\C#\damer 2013-2014";
            if (Directory.Exists(root)) App.ConfigService.Update("root", root);
            else App.ConfigService.Update("root", @"C:\Users\MFH\vhk");
            InitializeComponent();
            DataContext = App.PlayerViewModel;
            App.PlayerViewModel.PlayFunction += PlayerViewModel_PlayFunction;
        }

        private void PlayerViewModel_PlayFunction(object sender, PlayerFunctionEventArgs e) {
            if (e.File.Type == FileType.Music) return;
            Viewer.Source = new Uri(e.File.FullPath);
            Viewer.Play();
            Console.WriteLine("Playing {0}", e.File.Name);
        }
    }
}
