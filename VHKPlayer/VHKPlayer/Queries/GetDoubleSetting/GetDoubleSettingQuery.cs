using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetDoubleSetting
{
    public class GetDoubleSettingQuery : IQuery<double>
    {
        public string SettingName { get; set; }
    }
}
