namespace VHKPlayer.Utility.Settings.Interfaces
{
    public interface ISetting
    {
        object this[string propertyName] { get; set; }

        void Save();
    }
}