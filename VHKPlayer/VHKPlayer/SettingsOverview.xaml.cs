﻿using System;
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

namespace VHKPlayer
{
    /// <summary>
    /// Interaction logic for SettingsOverview.xaml
    /// </summary>
    public partial class SettingsOverview : Window
    {

        public bool ShouldClose { get; set; }

        public SettingsOverview()
        {
            InitializeComponent();
            ShouldClose = false;
            this.Closing += MediaViewer_Closing;
        }

        private void MediaViewer_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !ShouldClose;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
