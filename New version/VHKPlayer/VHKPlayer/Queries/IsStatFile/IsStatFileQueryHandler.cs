using System.Linq;
using VHKPlayer.Queries.GetFolderByName;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility;

namespace VHKPlayer.Queries.IsStatFile
{
    class IsStatFileQueryHandler : IQueryHandler<IsStatFileQuery, bool>
    {
        private readonly IQueryProcessor processor;

        public IsStatFileQueryHandler(IQueryProcessor processor)
        {
            this.processor = processor;
        }

        public bool Handle(IsStatFileQuery query)
        {
            var folder = processor.Process(new GetFolderByNameQuery()
            {
                Name = Constants.PlayerStatPictureFolderSettingName
            });

            return folder.Content.Any(x => x.FullPath == query.File.FullPath);
        }
    }
}
