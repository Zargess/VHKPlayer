using Autofac;
using System.Collections.ObjectModel;
using VHKPlayer.Commands.GUI;
using VHKPlayer.Commands.Logic.AddDataObserver;
using VHKPlayer.Commands.Logic.CreateAllPlayables;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.DataManagement.Interfaces;
using VHKPlayer.Infrastructure;
using VHKPlayer.Infrastructure.Modules;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Queries.GetAllPlayables;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.ViewModels
{
    public class PlayerViewModel : IDataObserver
    {
        private IContainer container;
        private ICommandProcessor cprocessor;
        private IQueryProcessor qprocessor;
        private readonly IDataMonitor monitor;

        public ObservableCollection<IPlayable> Playables { get; private set; }

        public PlayerViewModel()
        {
            container = App.Container;
            cprocessor = container.Resolve<ICommandProcessor>();
            qprocessor = container.Resolve<IQueryProcessor>();

            Playables = new ObservableCollection<IPlayable>();

            monitor = container.Resolve<IDataMonitor>();

            cprocessor.ProcessTransaction(new AddDataObserverCommand()
            {
                Observer = this
            });
        }

        public void InitialiseData()
        {
            cprocessor.ProcessTransaction(new CreateAllPlayablesCommand());
        }

        public void DataUpdated()
        {
            Playables.Clear();
            Playables.AddAll(qprocessor.Process(new GetAllPlayablesQuery()));
        }

        public System.Windows.Input.ICommand DummyCommand
        {
            get
            {
                return new DummyCommand();
            } set
            {

            }
        }
    }
}
