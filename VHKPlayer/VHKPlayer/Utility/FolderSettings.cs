using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Interfaces;

namespace VHKPlayer.Utility {
    public class FolderSettings : ISettings {
        public object this[string propertyName] {
            get {
                return FolderProperties.Default[propertyName];
            }
            set {
                FolderProperties.Default[propertyName] = value;
            }
        }

        public void Save() {
            FolderProperties.Default.Save();
        }
    }
}
