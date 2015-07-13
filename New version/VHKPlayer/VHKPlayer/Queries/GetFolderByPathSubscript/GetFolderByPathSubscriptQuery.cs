using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetFolderByPathSubscript
{
    public class GetFolderByPathSubscriptQuery : IQuery<FolderNode>
    {
        public string PartialPath { get; set; }
    }
}
