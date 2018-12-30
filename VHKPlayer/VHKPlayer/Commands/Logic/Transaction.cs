using System.Collections.Generic;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Commands.Logic
{
    public class Transaction : ITransaction
    {
        private readonly ICommandProcessor _processor;
        private readonly IDataCenter _center;

        public Transaction(IDataCenter center, ICommandProcessor processor)
        {
            this._center = center;
            this._processor = processor;
        }

        public void Process(IEnumerable<ICommand> commands)
        {
            if (commands == null) return;

            foreach (var command in commands)
            {
                _processor.Process(command);
            }

            if (!_center.UncommitedChanges) return;

            _center.Commit();
        }
    }
}