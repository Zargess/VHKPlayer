using Autofac;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using VHKPlayer.Interpreter.Interfaces;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Converters
{
    public class UniversalVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter.GetType() != typeof(Script))
            {
                throw new NotImplementedException(); // TODO : Show a notifycation that a script was not inserted correctly into the converter
                return Visibility.Visible;
            }

            var script = parameter as IScript;
            var interpreter = App.Container.Resolve<IScriptInterpreter>();
            if (interpreter.Evaluate(script, value)) return Visibility.Visible;

            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
