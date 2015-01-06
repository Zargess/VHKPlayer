using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Zargess.VHKPlayer.Commands {
    public class RelayCommand : ICommand {
        private Action<object> _action;
        private Func<bool> _canEx;

        public RelayCommand(Action<object> action) {
            _action = action;
        }

        public RelayCommand(Action<object> action, Func<bool> canEx) : this(action) {
            _canEx = canEx;
        }

        public bool CanExecute(object parameter) {
            return _canEx == null || _canEx();
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter) {
            _action(parameter ?? "Hello World");
        }
    }
}