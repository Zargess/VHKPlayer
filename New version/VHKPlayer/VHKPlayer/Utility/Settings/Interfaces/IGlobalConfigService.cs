using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHKPlayer.Utility.Settings.Interfaces
{
    public interface IGlobalConfigService
    {
        object GetObject(string settingName);
        string GetString(string settingName);
        void Update(string settingName, object value);
    }
}
