using System.Collections.Generic;

namespace VHKPlayer.Commands.Logic.Interfaces
{
    public interface ITransaction
    {
        void Process(IEnumerable<ICommand> commands);
    }
}