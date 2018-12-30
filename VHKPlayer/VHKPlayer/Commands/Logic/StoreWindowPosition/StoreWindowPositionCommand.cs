using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models;

namespace VHKPlayer.Commands.Logic.StoreWindowPosition
{
    public class StoreWindowPositionCommand : ICommand
    {
        public WindowPosition Position { get; set; }
    }
}