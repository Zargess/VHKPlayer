using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetTabsFromStringSetting
{
    public class GetTabsFromStringSettingQuery : IQuery<IQueryable<ITab>>
    {
        public string SettingName { get; set; }
        public IEnumerable<IPlayable> Playables { get; set; } 
    }
}
