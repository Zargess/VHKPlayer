using System.Collections.Generic;
using System.Linq;
using VHKPlayer.Queries.GetStringSetting;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility;

namespace VHKPlayer.Queries.GetRequiredFolderPaths
{
    class GetRequiredFolderPathsQueryHandler : IQueryHandler<GetRequiredFolderPathsQuery, IQueryable<string>>
    {
        private readonly IQueryProcessor _processor;

        public GetRequiredFolderPathsQueryHandler(IQueryProcessor processor)
        {
            this._processor = processor;
        }

        public IQueryable<string> Handle(GetRequiredFolderPathsQuery query)
        {
            var paths = new List<string>();

            foreach (var setting in Constants.PlayerFolderPathSettingNames)
            {
                paths.Add(_processor.Process(new GetStringSettingQuery()
                {
                    SettingName = setting
                }));
            }

            var playableFilesFolderPaths = _processor.Process(new GetStringSettingQuery()
            {
                SettingName = Constants.PlayableFileFoldersSettingName
            }).Split(';');

            paths.AddRange(playableFilesFolderPaths);

            var playlistConstructionStrings = _processor.Process(new GetStringSettingQuery()
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