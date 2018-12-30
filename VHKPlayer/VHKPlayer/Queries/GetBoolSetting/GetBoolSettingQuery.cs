using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetBoolSetting
{
    public class GetBoolSettingQuery : IQuery<bool>
    {
        public string SettingName { get; set; }
    }
}