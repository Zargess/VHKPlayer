﻿using System.Linq;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetPlayablesAffectedByFolder
{
    public class GetPlayablesAffectedByFolderQuery : IQuery<IQueryable<IPlayable>>
    {
        public FolderNode Folder { get; set; }
    }
}