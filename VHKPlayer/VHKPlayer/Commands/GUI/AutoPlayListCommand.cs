using System;
using System.Windows.Controls;
using System.Windows.Media;
using VHKPlayer.Controllers.Interfaces;

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
            menuItem.Background = _controller.AutoPlayList ? Brushes.Green : Brushes.Red;
        }
    }
}