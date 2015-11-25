using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Commands.Logic.RegisterTimer
{
    class RegisterTimerCommandHandler : ICommandHandler<RegisterTimerCommand>
    {
        private readonly IDataCenter _center;

        public RegisterTimerCommandHandler(IDataCenter center)
        {
            this._center = center;
        }

        public void Handle(RegisterTimerCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
