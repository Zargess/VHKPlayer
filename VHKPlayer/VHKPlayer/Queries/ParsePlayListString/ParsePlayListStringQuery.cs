using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.CreatePlayList;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.ParsePlayListString
{
    public class ParsePlayListStringQuery : IQuery<IQueryable<CreatePlayListCommand>>
    {
        public string ConstructString { get; set; }
    }
}
