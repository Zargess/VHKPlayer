using Autofac;
using VHKPlayer.Commands.Logic.Interfaces;

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
    }
}