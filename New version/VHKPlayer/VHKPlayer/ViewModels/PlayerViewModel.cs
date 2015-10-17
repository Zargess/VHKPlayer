using Autofac;
using System.Collections.ObjectModel;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Infrastructure;
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

        public ObservableCollection<IPlayable> Playables { get; private set; }

        public PlayerViewModel(IContainer container)
        {
            this.container = container;
            cprocessor = container.Resolve<ICommandProcessor>();
            qprocessor = container.Resolve<IQueryProcessor>();

            Playables = new ObservableCollection<IPlayable>();            
        }

        public void DataUpdated()
        {
            Playables.Clear();
            Playables.AddAll(qprocessor.Process(new GetAllPlayablesQuery()));
        }
    }
}
