using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHKPlayer.Commands.Logic.Interfaces
{
    public interface ICommandProcessor
    {
        void Process(ICommand command);
    }
}
