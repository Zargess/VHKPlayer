using System.Linq;
using VHKPlayer.Models;
using VHKPlayer.Queries.GetFolders;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetFolderFromPath
{
    class GetFolderFromPathQueryHandler : IQueryHandler<GetFolderFromPathQuery, FolderNode>
    {
        private readonly IQueryProcessor _processor;

        public GetFolderFromPathQueryHandler(IQueryProcessor processor)
        {
            this._processor = processor;
        }

        public FolderNode Handle(GetFolderFromPathQuery query)
        {
            var folders = _processor.Process(new GetFoldersQuery());
            return folders.SingleOrDefault(x => x.FullPath.ToLower() == query.Path.ToLower());
        }
    }
}