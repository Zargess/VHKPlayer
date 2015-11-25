using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetPlayableFiles
{
    class GetPlayableFilesQueryHandler : IQueryHandler<GetPlayableFilesQuery, IQueryable<PlayableFile>>
    {
        private readonly IDataCenter _center;

        public GetPlayableFilesQueryHandler(IDataCenter center)
        {
            this._center = center;
        }

        public IQueryable<PlayableFile> Handle(GetPlayableFilesQuery query)
        {
            return _center.PlayableFiles.AsQueryable();
        }
    }
}
