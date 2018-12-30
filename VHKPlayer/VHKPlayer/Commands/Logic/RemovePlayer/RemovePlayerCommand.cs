using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models;

namespace VHKPlayer.Commands.Logic.RemovePlayer
{
    public class RemovePlayerCommand : ICommand
    {
        public Player Player { get; set; }
    }
}