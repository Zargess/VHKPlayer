using System;
using System.Windows.Input;

namespace VHKPlayer.Commands.GUI
{
    public class DoNothingCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Console.WriteLine("Din mor!");
        }
    }
}