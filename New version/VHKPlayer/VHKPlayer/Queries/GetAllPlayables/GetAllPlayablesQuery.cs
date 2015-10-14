using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetAllPlayables
{
    public class GetAllPlayablesQuery : IQuery<IQueryable<IPlayable>>
    {
    }
}
