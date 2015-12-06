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
        public Data Data { get; set; }
        public PlayerViewModel ViewModel { get; set; }
        public MainWindow()
        {
            Data = new Data();
            ViewModel = new PlayerViewModel();
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            App.Dispatch = this.Dispatcher;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = ViewModel;
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var cprocessor = App.Container.Resolve<ICommandProcessor>();
            var qprocessor = App.Container.Resolve<IQueryProcessor>();
            var tab = ",{Test;LeftMain;2;(type name:PlayList);True;SingleFile}";
            var tabdefs = qprocessor.Process(new GetStringSettingQuery()
            {
                SettingName = Constants.TabsSettingName
            });
            cprocessor.Process(new ChangeSettingCommand()
            {
                SettingName = Constants.TabsSettingName,
                Value = tabdefs + tab
            });
        }

        private void MenuItem_OnClick2(object sender, RoutedEventArgs e)
        {
            var cprocessor = App.Container.Resolve<ICommandProcessor>();
            var qprocessor = App.Container.Resolve<IQueryProcessor>();

            var tabdefs = qprocessor.Process(new GetStringSettingQuery()
            {
                SettingName = Constants.TabsSettingName
            }).Split(',');

            if (tabdefs[tabdefs.Length - 1].StartsWith("{Test"))
            {
                var newDefs = "";
                for (var i = 0; i < tabdefs.Length - 1; i++)
                {
                    newDefs += tabdefs[i] + ",";
                }
                newDefs = newDefs.TrimEnd(',');
                cprocessor.Process(new ChangeSettingCommand()
                {
                    SettingName = Constants.TabsSettingName,
                    Value = newDefs
                });
            }

        }

        private void MenuItem_OnClick3(object sender, RoutedEventArgs e)
        {
            var cprocessor = App.Container.Resolve<ICommandProcessor>();
            var qprocessor = App.Container.Resolve<IQueryProcessor>();

            var tabdefs = qprocessor.Process(new GetStringSettingQuery()
            {
                SettingName = Constants.TabsSettingName
            }).Split('}');

            var newDefs = tabdefs.Aggregate("", (current, tabdef) => current + (tabdef + "},")).TrimEnd(',');

            cprocessor.Process(new ChangeSettingCommand()
            {
                SettingName = Constants.TabsSettingName,
                Value = newDefs
            });
        }
    }

    public class Data : IDataObserver
    {
        public ObservableCollection<IPlayable> Test { get; set; }
        private IDataMonitor _monitor;
        private IContainer _container;
        private readonly ICommandProcessor _cprocessor;
        private readonly IQueryProcessor _qprocessor;

        public Data()
        {
            Test = new ObservableCollection<IPlayable>();

            var builder = new ContainerBuilder();
            builder.RegisterModule(new DefaultWiringModule());
            _container = builder.Build();

            this._cprocessor = _container.Resolve<ICommandProcessor>();
            this._qprocessor = _container.Resolve<IQueryProcessor>();

            var path = @"C:\Users\Marcus\Dropbox\Programmering\C#\vhk";
            _cprocessor.ProcessTransaction(new ChangeSettingCommand()
            {
                SettingName = Constants.RootFolderPathSettingName,
                Value = path
            });

            _monitor = _container.Resolve<IDataMonitor>();

            _cprocessor.ProcessTransaction(new AddDataObserverCommand()
            {
                Observer = this
            });
        }

        public void InitialiseData()
        {
            this._cprocessor.ProcessTransaction(new CreateAllPlayablesCommand());
        }

        public void DataUpdated()
        {
            Test.Clear();

            var videos = _qprocessor.Process(new GetPlayableFilesQuery()).Where(x => x.File.Type == FileType.Video);
            var players = _qprocessor.Process(new GetPlayersQuery());
            var playlists = _qprocessor.Process(new GetPlayListsQuery()).ToList();

            Test.AddAll(playlists);
            Test.AddAll(videos);
            Test.AddAll(players);
        }
    }
}
