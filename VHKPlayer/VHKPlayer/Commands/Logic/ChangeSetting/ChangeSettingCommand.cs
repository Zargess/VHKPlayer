using VHKPlayer.Commands.Logic.Interfaces;

namespace VHKPlayer.Commands.Logic.ChangeSetting
{
    public class ChangeSettingCommand : ICommand
    {
        public string SettingName { get; set; }
        public object Value { get; set; }
    }
}