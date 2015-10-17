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
using VHKPlayer.DataManagement;
using VHKPlayer.DataManagement.Interfaces;
using VHKPlayer.Commands.Logic;

namespace VHKPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Data Data { get; set; }
        public MainWindow()
        {
            Data = new Data();
            InitializeComponent();
            this.DataContext = Data;
            this.Loaded += MainWindow_Loaded;
            App.Dispatch = this.Dispatcher;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Data.InitialiseData();
        }
    }

    public class Data : IDataObserver
    {
        public ObservableCollection<IPlayable> Test { get; set; }
        private IDataMonitor monitor;
        private IContainer container;
        private readonly ICommandProcessor cprocessor;
        private readonly IQueryProcessor qprocessor;

        public Data()
        {
            Test = new ObservableCollection<IPlayable>();

            var builder = new ContainerBuilder();
            builder.RegisterModule(new DefaultWiringModule());
            container = builder.Build();

            this.cprocessor = container.Resolve<ICommandProcessor>();
            this.qprocessor = container.Resolve<IQueryProcessor>();

            var path = @"C:\Users\Marcus\Dropbox\Programmering\C#\vhk";
            cprocessor.ProcessTransaction(new ChangeSettingCommand()
            {
                SettingName = Constants.RootFolderPathSettingName,
                Value = path
            });

            monitor = container.Resolve<IDataMonitor>();

            cprocessor.ProcessTransaction(new AddDataObserverCommand()
            {
                Observer = this
            });
        }

        public void InitialiseData()
        {
            this.cprocessor.ProcessTransaction(new CreateAllPlayablesCommand());
        }

        public void DataUpdated()
        {
            Test.Clear();

            var videos = qprocessor.Process(new GetPlayableFilesQuery()).Where(x => x.File.Type == FileType.Video);
            var players = qprocessor.Process(new GetPlayersQuery());
            var playlists = qprocessor.Process(new GetPlayListsQuery()).ToList();

            Test.AddAll(playlists);
            Test.AddAll(videos);
            Test.AddAll(players);
        }
    }
}
