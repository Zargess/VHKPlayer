using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models;
using VHKPlayer.Queries.GetFolderByPathSubscript;
using VHKPlayer.Queries.GetFolders;
using VHKPlayer.Queries.GetStringSetting;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility;

namespace VHKPlayer.Queries.GetFolderFromStringSetting
{
    class GetFolderFromStringSettingQueryHandler : IQueryHandler<GetFolderFromStringSettingQuery, FolderNode>
    {
        private readonly IQueryProcessor processor;

        public GetFolderFromStringSettingQueryHandler(IQueryProcessor processor)
        {
            this.processor = processor;
        }

        public FolderNode Handle(GetFolderFromStringSettingQuery query)
        {
            
            var setting = processor.Process(new GetStringSettingQuery()
            {
                SettingName = query.SettingName
            });

            var rootPath = processor.Process(new GetStringSettingQuery()
            {
                SettingName = Constants.RootFolderPathSettingName
            });

            var path = setting.Replace("root\\", "");

            if (Directory.Exists(setting))
            {
                var folders = processor.Process(new GetFoldersQuery()).Where(x => x.FullPath.ToLower() == setting.ToLower()).ToList();
                return folders.Single(x => x.FullPath.ToLower() == setting.ToLower());
            }

            var folder = processor.Process(new GetFolderByPathSubscriptQuery()
            {
                PartialPath = path
            });

            return folder;
        }
    }
}
