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
        }
    }
}
