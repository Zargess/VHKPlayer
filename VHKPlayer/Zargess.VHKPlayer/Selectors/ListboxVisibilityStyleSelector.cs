using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.Model;

namespace Zargess.VHKPlayer.Selectors {
    public class ListboxVisibilityStyleSelector : StyleSelector {
        public Style MusicTemplate { get; set; }
        public Style GeneralTemplate { get; set; }
        public Style GeneralPlayerTemplate { get; set; }

        public override Style SelectStyle(object item, DependencyObject container) {
            if (item is IPlayer) {
                var player = (IPlayer)item;
                if (player.Trainer) return GeneralPlayerTemplate;
            } else if (item is SingleItemPlayable) {
                var single = (SingleItemPlayable)item;
                var element = single.GetContent()[0];
                if (element.Type == FileType.Music) return MusicTemplate;
            }
            return GeneralTemplate;
        }
    }
}
