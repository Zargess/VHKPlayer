using System.Collections.Generic;
using Autofac;
using VHKPlayer.Commands.Logic.Interfaces;

namespace VHKPlayer.Commands.Logic
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly IComponentContext _context;

        public CommandProcessor(IComponentContext context)
        {
            this._context = context;
        }

        public void Process(ICommand command)
        {
            var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
            dynamic handler = _context.Resolve(handlerType);

            handler.Handle((dynamic) command);
        }

        public void ProcessTransaction(IEnumerable<ICommand> commands)
        {
            _context.Resolve<ITransaction>().Process(commands);
        }

        public void ProcessTransaction(ICommand command)
        {
            _context.Resolve<ITransaction>().Process(new[]
            {
                command
            });
        }
    }
}