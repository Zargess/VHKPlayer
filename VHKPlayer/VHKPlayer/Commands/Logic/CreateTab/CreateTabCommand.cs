using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Commands.Logic.CreateTab
{
    public class CreateTabCommand : ICommand
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public TabPlacement Placement { get; set; }
        public IPlayStrategy PlayStrategy { get; set; }
        public bool PlayListTab { get; set; }
        public IScript Script { get; set; }
    }
}