using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetIntSetting
{
    public class GetIntSettingQuery : IQuery<int>
    {
        public string SettingName { get; set; }
    }
}