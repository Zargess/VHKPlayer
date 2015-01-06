using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.Model;
using Zargess.VHKPlayer.Utility;

namespace Zargess.VHKPlayer.Converters {
    public class PlayerStatVisibilityConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (!(value is IPlayer)) return Visibility.Collapsed;

            var player = (IPlayer)value;

            if (player.Trainer) return Visibility.Collapsed;

            var statPicFolder = new FolderNode(PathHandler.AbsolutePath(App.ConfigService.GetPathString("playerFolders", 2)));
            statPicFolder.StopWatcher();

            var found = false;
            foreach (var file in player.Content) {
                if (!file.FullPath.ToLower().Contains(statPicFolder.FullPath.ToLower())) continue;
                if (!(file.Source.ToLower() == statPicFolder.Name.ToLower())) continue;
                found = true;
                break;
            }

            return found ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
