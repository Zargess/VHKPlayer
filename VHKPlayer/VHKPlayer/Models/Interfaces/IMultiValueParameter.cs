using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Models.Interfaces
{
    public interface IMultiValueParameter
    {
        IPlayable Playable { get; set; }
        IPlayStrategy Strategy { get; set; }
    }
}