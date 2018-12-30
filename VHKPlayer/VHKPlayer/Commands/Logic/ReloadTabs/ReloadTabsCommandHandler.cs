using VHKPlayer.Commands.Logic.CreateAllTabs;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Commands.Logic.RemoveAllTabs;

namespace VHKPlayer.Commands.Logic.ReloadTabs
{
    class ReloadTabsCommandHandler : ICommandHandler<ReloadTabsCommand>
    {
        private readonly ICommandProcessor _processor;

        public ReloadTabsCommandHandler(ICommandProcessor processor)
        {
            _processor = processor;
        }

        public void Handle(ReloadTabsCommand command)
        {
            _processor.Process(new RemoveAllTabsCommand());
            _processor.Process(new CreateAllTabsCommand());
        }
    }
}