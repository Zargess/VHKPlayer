using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.Model;

namespace Zargess.VHKPlayer.Converters {
    public class MusicVisibilityConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var element = value as SingleItemPlayable;
            if (element == null) return Visibility.Collapsed;
            var file = element.GetContent()[0];
            if (file.Type != FileType.Music) return Visibility.Collapsed;
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
