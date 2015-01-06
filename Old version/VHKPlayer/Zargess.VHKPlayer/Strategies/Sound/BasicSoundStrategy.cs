using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.Strategies.Sound {
    public class BasicSoundStrategy : ISoundStrategy {
        public void Starting() { }

        public void Stoping(Action callBack) { }
    }
}
