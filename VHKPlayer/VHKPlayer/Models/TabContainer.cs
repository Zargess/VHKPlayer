using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Models
{
    public class TabContainer : ITabContainer
    {
        public TabContainer()
        {
            LeftMain = new ObservableCollection<ITab>();
            RightMain = new ObservableCollection<ITab>();
            LeftDuringMatch = new ObservableCollection<ITab>();
            RightDuringMatch = new ObservableCollection<ITab>();
        }

        public ICollection<ITab> LeftMain { get; }
        public ICollection<ITab> RightMain { get; }
        public ICollection<ITab> LeftDuringMatch { get; }
        public ICollection<ITab> RightDuringMatch { get; }

        public ICollection<ITab> GetCollectionFromPlacement(TabPlacement placement)
        {
            switch (placement)
            {
                case TabPlacement.LeftMain:
                    return LeftMain;
                case TabPlacement.RightMain:
                    return RightMain;
                case TabPlacement.LeftDuringMatch:
                    return LeftDuringMatch;
                case TabPlacement.RightDuringMatch:
                    return RightDuringMatch;
                default:
                    throw new ArgumentException("Illegal argument was given.");
            }
        }
    }
}