using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Commands.Logic.UnregisterTimer
{
    class UnregisterTimerCommandHandler : ICommandHandler<UnregisterTimerCommand>
    {
        private readonly IDataCenter _center;

        public UnregisterTimerCommandHandler(IDataCenter center)
        {
            this._center = center;
        }

        public void Handle(UnregisterTimerCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
