using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetFolderFromStringSetting
{
    public class GetFolderFromStringSettingQuery : IQuery<FolderNode>
    {
        public string SettingName { get; set; }
    }
}
