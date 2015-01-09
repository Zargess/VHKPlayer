using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHKPlayer.Interfaces {
    public interface IGlobalConfigService : INotifyPropertyChanged {
        void Update(string settingName, object value);
        object Get(string settingName);
        string GetString(string settingName);
    }
}
