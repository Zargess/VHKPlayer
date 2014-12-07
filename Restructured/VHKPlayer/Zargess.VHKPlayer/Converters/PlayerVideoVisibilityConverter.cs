using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.Utility;

namespace Zargess.VHKPlayer.Converters {
    public class PlayerVideoVisibilityConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (!(value is IPlayer)) return Visibility.Collapsed;

            var player = (IPlayer)value;

            if (player.Trainer) return Visibility.Collapsed;

            var temp = App.ConfigService.GetPathString("playerFolders", 1);
            var vidFolderPath = PathHandler.AbsolutePath(temp).ToLower() + @"\";

            return player.Content.Any(x => x.FullPath.ToLower().Contains(vidFolderPath)) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
