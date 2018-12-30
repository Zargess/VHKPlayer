using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Utility.Settings.Interfaces;

namespace VHKPlayer.Commands.Logic.ChangeSetting
{
    class ChangeSettingCommandHandler : ICommandHandler<ChangeSettingCommand>
    {
        private readonly IGlobalConfigService _config;

        public ChangeSettingCommandHandler(IGlobalConfigService config)
        {
            this._config = config;
        }

        public void Handle(ChangeSettingCommand command)
        {
            _config.Update(command.SettingName, command.Value);
        }
    }
}