using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetStringSetting
{
    public class GetStringSettingQuery : IQuery<string>
    {
        public string SettingName { get; set; }
    }
}