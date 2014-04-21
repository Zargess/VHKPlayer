using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.FileManagement {
    public class PlayAbleContainer : IContainer {
        public List<IPlayAble> FileContainers { get; private set; }
        public string Name { get; private set; }

        public PlayAbleContainer(string name) {
            // TODO: Find ud af hvordan du får alting loaded og hvordan du gør det muligt at reloade
            Name = name;
        }

        public void Refresh() {
            
        }
    }
}