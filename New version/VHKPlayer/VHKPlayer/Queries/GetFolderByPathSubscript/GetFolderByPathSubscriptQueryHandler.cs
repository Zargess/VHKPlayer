using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models;
using VHKPlayer.Queries.GetFolders;
using VHKPlayer.Queries.GetStringSetting;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility;

namespace VHKPlayer.Queries.GetFolderByPathSubscript
{
    class GetFolderByPathSubscriptQueryHandler : IQueryHandler<GetFolderByPathSubscriptQuery, FolderNode>
    {
        private readonly IQueryProcessor processor;

        public GetFolderByPathSubscriptQueryHandler(IQueryProcessor processor)
        {
            this.processor = processor;
        }

        public FolderNode Handle(GetFolderByPathSubscriptQuery query)
        {
            var folders = processor.Process(new GetFoldersQuery()).ToList();

            var rootpath = processor.Process(new GetStringSettingQuery()
            {
                SettingName = Constants.RootFolderPathSettingName
            });

            var path = Path.Combine(rootpath, query.PartialPath);

            var folder = folders.SingleOrDefault(x => x.FullPath.ToLower() == path.ToLower());
            return folder;
        }
    }
}
