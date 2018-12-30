using VHKPlayer.Commands.Logic.CreatePlayList;
using VHKPlayer.Commands.Logic.Interfaces;

namespace VHKPlayer.Commands.Logic.CreateAutoPlayList
{
    public class CreateAutoPlayListCommand : ICommand
    {
        public CreatePlayListCommand Command { get; set; }
    }
}