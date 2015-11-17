using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            if (query.StrategyName == "AllFiles")
            {
                return new AllFilesPlayStrategy();
            }
            if (query.StrategyName == "Iterated")
            {
                return new IteratedPlayStrategy();
            }
            if (query.StrategyName == "SingleFile")
            {
                return new SingleFilePlayStrategy();
            }

            throw new NoSuchPlayStrategyException();
        }
    }
}
