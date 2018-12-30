using System.Collections.Generic;
using System.Linq;
using VHKPlayer.Models;
using VHKPlayer.Queries.GetFolderByPathSubscript;
using VHKPlayer.Queries.GetStringSetting;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility;

namespace VHKPlayer.Queries.GetPlayableFileFolders
{
    class GetPlayableFileFoldersQueryHandler : IQueryHandler<GetPlayableFileFoldersQuery, IQueryable<FolderNode>>
    {
        private readonly IQueryProcessor _processor;

        public GetPlayableFileFoldersQueryHandler(IQueryProcessor processor)
        {
            this._processor = processor;
        }

        public IQueryable<FolderNode> Handle(GetPlayableFileFoldersQuery query)
        {
            var res = new List<FolderNode>();

            var setting = _processor.Process(new GetStringSettingQuery()
            {
                SettingName = Constants.PlayableFileFoldersSettingName
            });

            var relativePaths = setting.Split(';');

            foreach (var relativePath in relativePaths)
            {
                var partialPath = relativePath.Replace("root\\", "");

                var folder = _processor.Process(new GetFolderByPathSubscriptQuery()
                {
                    PartialPath = partialPath
                });

                if (folder == null) continue;

                res.Add(folder);
            }

            return res.AsQueryable();
        }
    }
}