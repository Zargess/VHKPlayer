using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetBoolSetting
{
    public class GetBoolSettingQuery : IQuery<bool>
    {
        public string SettingName { get; set; }
    }
}
