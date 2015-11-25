using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetIntSetting
{
    public class GetIntSettingQuery : IQuery<int>
    {
        public string SettingName { get; set; }
    }
}
