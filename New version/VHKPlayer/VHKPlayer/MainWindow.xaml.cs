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
            

            SampleData.Add(42);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Data.DataUpdated(PlayableType.PlayableFile);
        }

        ObservableCollection<int> sampleData = new ObservableCollection<int>();
        public ObservableCollection<int> SampleData
        {
            get
            {
                if (sampleData.Count <= 0)
                {
                    sampleData.Add(1);
                    sampleData.Add(2);
                    sampleData.Add(3);
                    sampleData.Add(4);
                }
                return sampleData;
            }
        }

    }

    public class Data : IDataObserver
    {
        public ObservableCollection<IPlayable> Test { get; set; }
        private IContainer container;
        public Data()
        {
            Test = new ObservableCollection<IPlayable>();

            var builder = new ContainerBuilder();
            builder.RegisterModule(new DefaultWiringModule());
            container = builder.Build();

            var cprocessor = container.Resolve<ICommandProcessor>();
            cprocessor.Process(new AddDataObserverCommand()
            {
                Observer = this
            });
        }

        public void DataUpdated(PlayableType type)
        {

            var cprocessor = container.Resolve<ICommandProcessor>();
            var qprocessor = container.Resolve<IQueryProcessor>();

            var path = @"C:\Users\Marcus\Dropbox\Programmering\C#\vhk";
            cprocessor.Process(new ChangeSettingCommand()
            {
                SettingName = Constants.RootFolderPathSettingName,
                Value = path
            });

            cprocessor.Process(new CreateAllPlayablesCommand());

            var playables = qprocessor.Process(new GetPlayableFilesQuery()).ToList();
            foreach (var playable in playables)
            {
                Test.Add(playable);
            }
        }
    }
}
