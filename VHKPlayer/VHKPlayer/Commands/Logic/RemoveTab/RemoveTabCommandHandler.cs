using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Commands.Logic.RemoveTab
{
    public class RemoveTabCommandHandler : ICommandHandler<RemoveTabCommand>
    {
        private readonly ITabContainer _container;

        public RemoveTabCommandHandler(ITabContainer container)
        {
            _container = container;
        }

        public void Handle(RemoveTabCommand command)
        {
            _container.Tabs.Remove(command.Tab);
        }
    }
}
