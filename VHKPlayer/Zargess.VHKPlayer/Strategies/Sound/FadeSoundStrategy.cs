using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.Strategies.Sound {
    public class FadeSoundStrategy : ISoundStrategy {
        private DoubleAnimation Animation { get; set; }

        public FadeSoundStrategy() {

        }
            
        public void Starting() {
            throw new NotImplementedException();
        }

        public void Stoping() {
            throw new NotImplementedException();
        }
    }
}
