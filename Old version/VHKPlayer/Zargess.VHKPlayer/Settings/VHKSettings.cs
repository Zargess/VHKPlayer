using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.Settings {
    public class VHKSettings : ISettings {
        public object this[string propertyName] {
            get {
                return Properties.Default[propertyName];
            }
            set {
                Properties.Default[propertyName] = value;
            }
        }

        public void Save() {
            Properties.Default.Save();
        }
    }
}
