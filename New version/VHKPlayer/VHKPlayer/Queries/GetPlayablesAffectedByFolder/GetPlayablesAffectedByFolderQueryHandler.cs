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
        private readonly IQueryProcessor processor;

        public GetPlayablesAffectedByFolderQueryHandler(IQueryProcessor processor)
        {
            this.processor = processor;
        }

        public IQueryable<IPlayable> Handle(GetPlayablesAffectedByFolderQuery query)
        {
            var res = new List<IPlayable>();

            var playableFiles = processor.Process(new GetPlayableFilesQuery());

            foreach (var playableFile in playableFiles)
            {
                if (query.Folder.Contains(playableFile.File))
                {
                    res.Add(playableFile);
                }
            }

            var playerFolders = processor.Process(new GetPlayerFoldersQuery());

            foreach (var folder in playerFolders)
            {
                if (query.Folder.FullPath.ToLower().Equals(folder.FullPath.ToLower()))
                {
                    res.AddRange(processor.Process(new GetPlayersQuery()));
                    break;
                }
            }

            return res.AsQueryable();
        }
    }
}
