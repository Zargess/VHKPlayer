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
        private readonly IQueryProcessor _processor;

        public GetFolderFromStringSettingQueryHandler(IQueryProcessor processor)
        {
            this._processor = processor;
        }

        public FolderNode Handle(GetFolderFromStringSettingQuery query)
        {
            
            var setting = _processor.Process(new GetStringSettingQuery()
            {
                SettingName = query.SettingName
            });

            var rootPath = _processor.Process(new GetStringSettingQuery()
            {
                SettingName = Constants.RootFolderPathSettingName
            });

            if (String.IsNullOrEmpty(rootPath)) return null;

            var path = setting.Replace("root\\", "");

            if (Directory.Exists(setting))
            {
                var folders = _processor.Process(new GetFoldersQuery()).Where(x => x.FullPath.ToLower() == setting.ToLower()).ToList();
                return folders.Single(x => x.FullPath.ToLower() == setting.ToLower());
            }

            var folder = _processor.Process(new GetFolderByPathSubscriptQuery()
            {
                PartialPath = path
            });

            return folder;
        }
    }
}
