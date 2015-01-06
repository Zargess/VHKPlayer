using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.Settings {
    public class GUISettings : ISettings {
        public object this[string propertyName] {
            get {
                return GuiSettings.Default[propertyName];
            }

            set {
                GuiSettings.Default[propertyName] = value;
            }
        }

        public void Save() {
            GuiSettings.Default.Save();
        }
    }
}
