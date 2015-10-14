using System;
using Autofac;
using VHKPlayer.Commands.Logic.Interfaces;
using System.Collections.Generic;

namespace VHKPlayer.Commands.Logic
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly IComponentContext context;

        public CommandProcessor(IComponentContext context)
        {
            this.context = context;
        }

        public void Process(ICommand command)
        {
            var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
            dynamic handler = context.Resolve(handlerType);

            handler.Handle((dynamic)command);
        }

        public void ProcessTransaction(IEnumerable<ICommand> commands)
        {
            context.Resolve<ITransaction>().Process(commands);
        }

        public void ProcessTransaction(ICommand command)
        {
            context.Resolve<ITransaction>().Process(new[]
            {
                command
            });
        }
    }
}