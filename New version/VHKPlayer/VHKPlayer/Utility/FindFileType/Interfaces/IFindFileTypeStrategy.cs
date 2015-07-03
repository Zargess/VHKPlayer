using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models;

namespace VHKPlayer.Utility.FindFileType.Interfaces
{
    public interface IFindFileTypeStrategy
    {
        FileType FindType(string path);
    }
}
