using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models;

namespace VHKPlayer.Utility.GetPlayerFolders.Interfaces
{
    public interface IGetPlayerFoldersStrategy
    {
        List<FolderNode> GetFolders();
    }
}
