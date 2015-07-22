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

namespace VHKPlayer.Queries.GetPlayerFolders
{
    class GetPlayerFoldersQueryHandler : IQueryHandler<GetPlayerFoldersQuery, IQueryable<FolderNode>>
    {
        private readonly IQueryProcessor processor;

        public GetPlayerFoldersQueryHandler(IQueryProcessor processor)
        {
            this.processor = processor;
        }

        public IQueryable<FolderNode> Handle(GetPlayerFoldersQuery query)
        {
            var settingNames = new[] {
                Constants.PlayerPictureFolderSettingName,
                Constants.PlayerVideoFolderSettingName,
                Constants.PlayerStatPictureFolderSettingName,
                Constants.PlayerStatVideoFolderSettingName,
                Constants.PlayerStatMusicFolderSettingName
            };

            var res = new List<FolderNode>();

            foreach (var setting in settingNames)
            {
                var relativePath = processor.Process(new GetStringSettingQuery()
                {
                    SettingName = setting
                });
                var partialPath = relativePath.Replace("root\\", "");
                res.Add(processor.Process(new GetFolderByPathSubscriptQuery()
                {
                    PartialPath = partialPath
                }));
            }

            return res.AsQueryable();
        }
    }
}
