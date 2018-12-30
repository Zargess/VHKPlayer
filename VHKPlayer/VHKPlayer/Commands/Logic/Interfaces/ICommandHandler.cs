namespace VHKPlayer.Commands.Logic.Interfaces
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        void Handle(TCommand command);
    }
}