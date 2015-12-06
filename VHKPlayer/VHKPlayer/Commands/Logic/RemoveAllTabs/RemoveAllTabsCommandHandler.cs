using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Commands.Logic.RemoveAllTabs
{
    public class RemoveAllTabsCommandHandler : ICommandHandler<RemoveAllTabsCommand>
    {
        private readonly ITabContainer _container;

        public RemoveAllTabsCommandHandler(ITabContainer container)
        {
            _container = container;
        }

        public void Handle(RemoveAllTabsCommand command)
        {
            _container.RightMain.Clear();
            _container.LeftMain.Clear();
            _container.LeftDuringMatch.Clear();
            _container.RightDuringMatch.Clear();
        }
    }
}
