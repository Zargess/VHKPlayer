using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Queries.GetStringSetting;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility;

namespace VHKPlayer.Queries.GetRequiredFolderPaths
{
    class GetRequiredFolderPathsQueryHandler : IQueryHandler<GetRequiredFolderPathsQuery, IQueryable<string>>
    {
        private readonly IQueryProcessor processor;

        public GetRequiredFolderPathsQueryHandler(IQueryProcessor processor)
        {
            this.processor = processor;
        }

        public IQueryable<string> Handle(GetRequiredFolderPathsQuery query)
        {
            var paths = new List<string>();

            foreach (var setting in Constants.PlayerFolderPathSettingNames)
            {
                paths.Add(processor.Process(new GetStringSettingQuery()
                {
                    SettingName = setting
                }));
            }

            var playableFilesFolderPaths = processor.Process(new GetStringSettingQuery()
            {
                SettingName = Constants.PlayableFileFoldersSettingName
            }).Split(';');

            paths.AddRange(playableFilesFolderPaths);

            var playlistConstructionStrings = processor.Process(new GetStringSettingQuery()
            {
                SettingName = Constants.PlayListsSettingName
            }).Split(',');

            foreach (var constring in playlistConstructionStrings)
            {
                var parts = constring.Split(';');
                paths.Add(parts[1]);
            }

            return paths.AsQueryable();
        }
    }
}
