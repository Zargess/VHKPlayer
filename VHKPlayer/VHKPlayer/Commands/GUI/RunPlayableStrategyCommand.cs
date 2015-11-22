using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Commands.GUI
{
    public class RunPlayableStrategyCommand : ICommand
    {
        private readonly IPlayStrategy strategy;

        public RunPlayableStrategyCommand(IPlayStrategy strategy)
        {
            this.strategy = strategy;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var playable = parameter as IPlayable;

            if (playable == null)
            {
                return;
            }

            
        }

        public event EventHandler CanExecuteChanged;
    }
}
