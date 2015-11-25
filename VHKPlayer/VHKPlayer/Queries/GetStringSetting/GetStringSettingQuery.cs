using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetStringSetting
{
    public class GetStringSettingQuery : IQuery<string>
    {
        public string SettingName { get; set; }
    }
}
