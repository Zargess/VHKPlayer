using VHKPlayer.Models;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetPlayListByName
{
    public class GetPlayListByNameQuery : IQuery<PlayList>
    {
        public string Name { get; set; }
    }
}