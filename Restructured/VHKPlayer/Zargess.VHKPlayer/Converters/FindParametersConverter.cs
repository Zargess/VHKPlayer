using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Zargess.VHKPlayer.Commands;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.Converters {
    public class FindCommandParameterConverter : IMultiValueConverter {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            var res = new FindParameters();
            foreach (var item in values) {
                if (item is IPlayable) res.Playable = (IPlayable)item;
                else if (item is string) res.ControlName = (string)item;
            }
            return res;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
