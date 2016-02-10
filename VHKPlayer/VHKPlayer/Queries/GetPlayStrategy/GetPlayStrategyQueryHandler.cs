using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.GUI;
using VHKPlayer.Exceptions;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility.PlayStrategy;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Queries.GetPlayStrategy
{
    class GetPlayStrategyQueryHandler : IQueryHandler<GetPlayStrategyQuery, IPlayStrategy>
    {
        public IPlayStrategy Handle(GetPlayStrategyQuery query)
        {
            switch (query.StrategyName)
            {
                case "AllFiles":
                    return new AllFilesPlayStrategy();
                case "Iterated":
                    return new IteratedPlayStrategy();
                case "SingleFile":
                    return new SingleFilePlayStrategy();
                case "PlayerPicture":
                    return new PlayerPicturePlayStrategy();
                case "PlayerStat":
                    return new PlayerStatPlayStrategy();
                default:
                    throw new NoSuchPlayStrategyException();
            }
        }
    }
}
