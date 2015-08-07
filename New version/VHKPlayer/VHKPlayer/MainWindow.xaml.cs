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

namespace VHKPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<IPlayable> Content { get; set; }

        public MainWindow()
        {
            Content = new ObservableCollection<IPlayable>();
            InitializeComponent();

            var builder = new ContainerBuilder();
            builder.RegisterModule(new DefaultWiringModule());
            var container = builder.Build();

            var cprocessor = container.Resolve<ICommandProcessor>();
            var qprocessor = container.Resolve<IQueryProcessor>();

            var path = @"C:\Users\Marcus\Dropbox\Programmering\C#\vhk";
            cprocessor.Process(new ChangeSettingCommand()
            {
                SettingName = Constants.RootFolderPathSettingName,
                Value = path
            });

            cprocessor.Process(new CreateFolderStructureCommand()
            {
                RootFolderPath = path
            });

            cprocessor.Process(new CreateAllPlayablesCommand());

            var playables = qprocessor.Process(new GetPlayableFilesQuery());
            // TODO : Implement a FindFileTypeStrategy
            foreach (var playable in playables)
            {
                Content.Add(playable);
            }
        }
    }
}
