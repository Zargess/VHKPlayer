using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Utility.GetSpecialTab.Interfaces
{
    public interface IGetSpecialTabStrategy
    {
        bool IsSpecialTab(string def);
        ITab CreateSpecialTab(string name);
    }
}