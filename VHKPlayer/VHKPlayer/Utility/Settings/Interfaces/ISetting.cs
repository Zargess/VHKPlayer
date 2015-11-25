using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHKPlayer.Utility.Settings.Interfaces
{
    public interface ISetting
    {
        object this[string propertyName] { get; set; }

        void Save();
    }
}
