using System;
using System.Windows.Input;
using VHKPlayer.MessageBoxes;

namespace VHKPlayer.Commands.GUI
{
    public class DummyCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var alert = new AlertBox();
            alert.Show();
        }
    }
}