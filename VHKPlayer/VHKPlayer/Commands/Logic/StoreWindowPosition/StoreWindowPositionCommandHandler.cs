using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Utility;
using VHKPlayer.Utility.Settings.Interfaces;

namespace VHKPlayer.Commands.Logic.StoreWindowPosition
{
    class StoreWindowPositionCommandHandler : ICommandHandler<StoreWindowPositionCommand>
    {
        private readonly IGlobalConfigService _config;

        public StoreWindowPositionCommandHandler(IGlobalConfigService config)
        {
            _config = config;
        }

        public void Handle(StoreWindowPositionCommand command)
        {
            _config.Update(Constants.TopSettingName, command.Position.Top);
            _config.Update(Constants.LeftSettingName, command.Position.Left);
            _config.Update(Constants.HeightSettingName, command.Position.Height);
            _config.Update(Constants.WidthSettingName, command.Position.Width);
            _config.Update(Constants.MaximizedSettingName, command.Position.Maximized);
        }
    }
}
