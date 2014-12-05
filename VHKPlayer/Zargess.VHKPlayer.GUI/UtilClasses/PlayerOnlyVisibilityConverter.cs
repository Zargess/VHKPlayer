using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Zargess.VHKPlayer.FileManagement.Interfaces;

namespace Zargess.VHKPlayer.GUI.UtilClasses {
    public class PlayerOnlyVisibilityConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (!(value is IPlayer)) return Visibility.Collapsed;
            var element = (IPlayer)value;
            if (element.Trainer) return Visibility.Collapsed;
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
