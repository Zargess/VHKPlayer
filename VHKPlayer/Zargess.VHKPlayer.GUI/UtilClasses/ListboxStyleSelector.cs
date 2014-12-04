using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Zargess.VHKPlayer.FileManagement;
using Zargess.VHKPlayer.FileManagement.Interfaces;

namespace Zargess.VHKPlayer.GUI.UtilClasses {
    public class ListboxStyleSelector : StyleSelector {
        public Style MusicTemplate { get; set; }
        public Style GeneralTemplate { get; set; }
        public Style GeneralPlayerTemplate { get; set; }
        public Style PlayerTemplate { get; set; }

        public override Style SelectStyle(object item, DependencyObject container) {
            if(item is IPlayer) {
                var player = (IPlayer)item;
                if (player.Trainer) return GeneralPlayerTemplate;
                return PlayerTemplate;
            }
            if(item is SingleItemPlayable) {
                var single = (SingleItemPlayable)item;
                var element = single.GetContent()[0];
                if (element.Type == FileType.Music) return MusicTemplate;
            }
            return GeneralTemplate;
        }
    }
}
