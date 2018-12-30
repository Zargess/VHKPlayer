using System.Linq;
using VHKPlayer.Commands.Logic.CreatePlayList;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.ParsePlayListString
{
    public class ParsePlayListStringQuery : IQuery<IQueryable<CreatePlayListCommand>>
    {
        public string ConstructString { get; set; }
    }
}