using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetDoubleSetting
{
    public class GetDoubleSettingQuery : IQuery<double>
    {
        public string SettingName { get; set; }
    }
}