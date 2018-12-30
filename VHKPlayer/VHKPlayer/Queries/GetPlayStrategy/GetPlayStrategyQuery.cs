using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Queries.GetPlayStrategy
{
    public class GetPlayStrategyQuery : IQuery<IPlayStrategy>
    {
        public string StrategyName { get; set; }
    }
}