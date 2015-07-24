using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models;
using VHKPlayer.Queries.GetFolderByPathSubscript;
using VHKPlayer.Queries.GetStringSetting;
using VHKPlayer.Queries.Interfaces;

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

            var partialPath = setting.Replace("root\\", "");

            var folder = processor.Process(new GetFolderByPathSubscriptQuery()
            {
                PartialPath = partialPath
            });

            return folder;
        }
    }
}
