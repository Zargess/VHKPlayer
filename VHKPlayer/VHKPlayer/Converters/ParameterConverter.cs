using System;
using System.Globalization;
using System.Windows.Data;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Converters
{
    public class ParameterConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var res = new MultiValueParameter();
            ;

            foreach (var item in values)
            {
                if (item is IPlayStrategy) res.Strategy = (IPlayStrategy) item;
                else if (item is IPlayable) res.Playable = (IPlayable) item;
            }

            return res;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}