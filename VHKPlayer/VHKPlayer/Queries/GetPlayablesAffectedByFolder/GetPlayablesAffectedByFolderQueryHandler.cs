using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Queries.GetPlayableFiles;
using VHKPlayer.Queries.GetPlayerFolders;
using VHKPlayer.Queries.GetPlayers;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetPlayablesAffectedByFolder
{
    class GetPlayablesAffectedByFolderQueryHandler : IQueryHandler<GetPlayablesAffectedByFolderQuery, IQueryable<IPlayable>>
    {
        private readonly IQueryProcessor _processor;

        public GetPlayablesAffectedByFolderQueryHandler(IQueryProcessor processor)
        {
            this._processor = processor;
        }

        public IQueryable<IPlayable> Handle(GetPlayablesAffectedByFolderQuery query)
        {
            var res = new List<IPlayable>();

            var playableFiles = _processor.Process(new GetPlayableFilesQuery());

            foreach (var playableFile in playableFiles)
            {
                if (query.Folder.Contains(playableFile.File) || playableFile.File.FullPath.ToLower().Contains(query.Folder.FullPath.ToLower()))
                {
                    res.Add(playableFile);
                }
            }

            var playerFolders = _processor.Process(new GetPlayerFoldersQuery());
            // TODO : Does not work if there is no players.....
            foreach (var folder in playerFolders)
            {
                if (query.Folder.FullPath.ToLower().Equals(folder.FullPath.ToLower()))
                {
                    var players = _processor.Process(new GetPlayersQuery());
                    if (players.Count() == 0) res.Add(new Player());
                    else res.AddRange(players);
                    break;
                }
            }

            return res.AsQueryable();
        }
    }
}
