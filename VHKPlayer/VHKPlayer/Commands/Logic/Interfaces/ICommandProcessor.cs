using System.Collections.Generic;

namespace VHKPlayer.Commands.Logic.Interfaces
{
    public interface ICommandProcessor
    {
        void Process(ICommand command);
        void ProcessTransaction(IEnumerable<ICommand> commands);
        void ProcessTransaction(ICommand command);
    }
}