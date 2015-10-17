using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetPlayListByName
{
    public class GetPlayListByNameQuery : IQuery<PlayList>
    {
        public string Name { get; set; }
    }
}
