using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VHKPlayer.Controllers.Interfaces;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Commands.GUI
{
    public class RunPlayableStrategyCommand : ICommand
    {
        private readonly IVideoPlayerController _controller;

        public RunPlayableStrategyCommand(IVideoPlayerController controller)
        {
            this._controller = controller;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var parameters = parameter as IMultiValueParameter;
            if (parameters == null)
            {
                return;
            }

            var playable = parameters.Playable;
            var strategy = parameters.Strategy;

            if (playable == null || strategy == null)
            {
                return;
            }

            _controller.Play(playable, strategy);

            // TODO : Remove this print
            Console.WriteLine("Run Playable Strategy Command called with: {0}", playable.Name);
        }

        public event EventHandler CanExecuteChanged;
    }
}
