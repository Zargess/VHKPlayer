using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models;
using VHKPlayer.Queries.GetFolderByPathSubscript;
using VHKPlayer.Queries.GetStringSetting;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility;

namespace VHKPlayer.Queries.GetPlayableFileFolders
{
    class GetPlayableFileFoldersQueryHandler : IQueryHandler<GetPlayableFileFoldersQuery, IQueryable<FolderNode>>
    {
        private readonly IQueryProcessor processor;

        public GetPlayableFileFoldersQueryHandler(IQueryProcessor processor)
        {
            this.processor = processor;
        }

        public IQueryable<FolderNode> Handle(GetPlayableFileFoldersQuery query)
        {
            var res = new List<FolderNode>();

            var setting = processor.Process(new GetStringSettingQuery()
            {
                SettingName = Constants.PlayableFileFoldersSettingName
            });

            var relativePaths = setting.Split(';');

            foreach (var relativePath in relativePaths)
            {
                var partialPath = relativePath.Replace("root\\", "");

                var folder = processor.Process(new GetFolderByPathSubscriptQuery()
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
