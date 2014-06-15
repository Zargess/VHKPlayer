using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Enums;

namespace Zargess.VHKPlayer.FileManagement {
    public class Playlist : IPlayable {
        public List<FileNode> Music { get; protected set; }
        public List<FileNode> Video { get; protected set; }
        public List<Procedure> Procedures { get; protected set; }
        public bool Repeatable { get; protected set; }


    }
}
