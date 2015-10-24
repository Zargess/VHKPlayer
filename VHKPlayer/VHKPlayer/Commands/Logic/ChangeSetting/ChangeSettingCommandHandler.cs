using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Utility.Settings.Interfaces;

namespace VHKPlayer.Commands.Logic.ChangeSetting
{
    class ChangeSettingCommandHandler : ICommandHandler<ChangeSettingCommand>
    {
        private readonly IGlobalConfigService config;

        public ChangeSettingCommandHandler(IGlobalConfigService config)
        {
            this.config = config;
        }

        public void Handle(ChangeSettingCommand command)
        {
            config.Update(command.SettingName, command.Value);
        }
    }
}
