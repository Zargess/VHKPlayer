using Autofac;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.AddDataObserver;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Commands.Logic.RemoveDataObserver;
using VHKPlayer.Infrastructure;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Queries.GetAllPlayables;
using VHKPlayer.Queries.GetStringSetting;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Queries.IsValidRootFolder;
using VHKPlayer.Utility;

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
            // TODO : Implement this later
        }
    }
}
