using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.IsStatFile
{
    public class IsStatFileQuery : IQuery<bool>
    {
        public FileNode File { get; set; }
    }
}
