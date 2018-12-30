using System.IO;
using VHKPlayer.Queries.GetRequiredFolderPaths;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.IsValidRootFolder
{
    class IsValidRootFolderQueryHandler : IQueryHandler<IsValidRootFolderQuery, bool>
    {
        private readonly IQueryProcessor _processor;

        public IsValidRootFolderQueryHandler(IQueryProcessor processor)
        {
            this._processor = processor;
        }

        public bool Handle(IsValidRootFolderQuery query)
        {
            var requiredPaths = _processor.Process(new GetRequiredFolderPathsQuery());

            foreach (var path in requiredPaths)
            {
                var newPath = path.Replace("root/", query.Path);
                if (!Directory.Exists(newPath)) return false;
            }

            return true;
        }
    }
}