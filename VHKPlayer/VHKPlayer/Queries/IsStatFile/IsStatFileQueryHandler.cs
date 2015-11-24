using System.Linq;
using VHKPlayer.Queries.GetFolderByPathSubscript;
using VHKPlayer.Queries.GetStringSetting;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility;

namespace VHKPlayer.Queries.IsStatFile
{
    class IsStatFileQueryHandler : IQueryHandler<IsStatFileQuery, bool>
    {
        private readonly IQueryProcessor _processor;

        public IsStatFileQueryHandler(IQueryProcessor processor)
        {
            this._processor = processor;
        }

        public bool Handle(IsStatFileQuery query)
        {
            var folder = _processor.Process(new GetFolderByPathSubscriptQuery()
            {
                PartialPath = _processor.Process(new GetStringSettingQuery()
                {
                    SettingName = Constants.PlayerStatPictureFolderSettingName
                }).Replace("root\\", "")
            });

            return folder.Content.Any(x => query.File.FullPath.ToLower() == x.FullPath.ToLower());
        }
    }
}
