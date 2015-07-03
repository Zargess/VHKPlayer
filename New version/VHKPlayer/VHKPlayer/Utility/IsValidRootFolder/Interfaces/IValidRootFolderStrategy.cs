using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models;

namespace VHKPlayer.Utility.IsValidRootFolder.Interfaces
{
    public interface IValidRootFolderStrategy
    {
        bool IsValidRootFolder(FolderNode folder);
    }
}
