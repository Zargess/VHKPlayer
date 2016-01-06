using Autofac;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using VHKPlayer.Commands.Logic.ChangeSetting;
using VHKPlayer.Commands.Logic.CreateAllPlayables;
using VHKPlayer.Commands.Logic.CreateFolderStructure;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Infrastructure.Modules;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Queries.GetPlayableFiles;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility;
using VHKPlayer.Models;
using VHKPlayer.Commands.Logic.AddDataObserver;
using VHKPlayer.Queries.GetPlayers;
using VHKPlayer.Commands.Logic.RemoveDataObserver;
using VHKPlayer.Queries.GetPlayLists;
using VHKPlayer.Infrastructure;
using VHKPlayer.Commands.Logic;
using VHKPlayer.Controllers;
using VHKPlayer.Monitors.Interfaces;
using VHKPlayer.Queries.GetStringSetting;
using VHKPlayer.ViewModels;

namespace VHKPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public PlayerViewModel ViewModel { get; set; }
        public PlayController Controller { get; set; }
        public MainWindow()
        {
            ViewModel = new PlayerViewModel();
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.Closing += MainWindow_Closing;
            this.KeyUp += MainWindow_KeyUp;
            App.Dispatch = this.Dispatcher;
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            // TODO : Make a lookup in a command library of some sort contained inside the viewmodel and changeable inside settings

            throw new NotImplementedException();
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ViewModel.Controller.Shutdown();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = ViewModel;
            View.Video.MediaEnded += (s, ee) => ViewModel.Controller.PlayQueue();
            Controller = new PlayController(View);
            ViewModel.Controller.AddObserver(Controller);
            
        }
    }
}
