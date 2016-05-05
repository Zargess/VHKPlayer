using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Controllers.Interfaces;
using VHKPlayer.Queries.GetBoolSetting;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility;

namespace VHKPlayer.Commands.GUI
{
    public class AutoPlayListCommand : System.Windows.Input.ICommand
    {
        private readonly IVideoPlayerController _controller;

        public event EventHandler CanExecuteChanged;

        public AutoPlayListCommand(IVideoPlayerController controller)
        {
            _controller = controller;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var menuItem = parameter as MenuItem;
            _controller.AutoPlayList = !_controller.AutoPlayList;
            if (_controller.AutoPlayList)
            {
                menuItem.Background = Brushes.Green;
            } else
            {
                menuItem.Background = Brushes.Red;
            }
        }
    }
}
