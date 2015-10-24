using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Commands.Logic
{
    public class Transaction : ITransaction
    {
        private readonly ICommandProcessor processor;
        private readonly IDataCenter center;

        public Transaction(IDataCenter center, ICommandProcessor processor)
        {
            this.center = center;
            this.processor = processor;
        }

        public void Process(IEnumerable<ICommand> commands)
        {
            if (commands == null) return;

            foreach (var command in commands)
            {
                processor.Process(command);
            }

            if (!center.UncommitedChanges) return;

            center.Commit();
        }
    }
}
