using VHKPlayer.Models;
using VHKPlayer.Queries.GetFolderByPathSubscript;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetFolderByRelativePath
{
    class GetFolderByRelativePathQueryHandler : IQueryHandler<GetFolderByRelativePathQuery, FolderNode>
    {
        private readonly IQueryProcessor _processor;

        public GetFolderByRelativePathQueryHandler(IQueryProcessor processor)
        {
            this._processor = processor;
        }

        public FolderNode Handle(GetFolderByRelativePathQuery query)
        {
            var partialpath = query.RelativePath.Replace("root\\", "");
            return _processor.Process(new GetFolderByPathSubscriptQuery()
            {
                PartialPath = partialpath
            });
        }
    }
}