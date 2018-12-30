using VHKPlayer.Exceptions;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Utility.GetSpecialTab.Interfaces;

namespace VHKPlayer.Utility.GetSpecialTab
{
    public class GetSpecialTabStrategy : IGetSpecialTabStrategy
    {
        public ITab CreateSpecialTab(string name)
        {
            if (name == "duringmatch")
            {
                return new DuringMatchTab
                {
                    Placement = TabPlacement.RightMain,
                    Number = int.MinValue,
                    Name = "Under Kamp"
                };
            }
            else
            {
                throw new NoSuchTabException("No such tab exists\n" + name);
            }
        }

        public bool IsSpecialTab(string def)
        {
            if (def == "duringmatch")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}