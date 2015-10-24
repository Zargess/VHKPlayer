using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VHKPlayer.Commands.GUI;

namespace VHKPlayer.ViewModels
{
    public class TestViewModel
    {
        public ICommand DummyCommand
        {
            get
            {
                return new DummyCommand();
            }
            set
            {

            }
        }
    }
}
