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
    public class UniversalVisibilityConverter : IMultiValueConverter
    {
        private int counter = 0;
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2) return Visibility.Hidden;

            var script = values.SingleOrDefault(x => x is IScript) as IScript;

            if (script == null)
            {
                throw new NotImplementedException(); // TODO : Show a notifycation that a script was not inserted correctly into the converter
                return Visibility.Collapsed;
            }

            var value = values.SingleOrDefault(x => x is IPlayable);

            var interpreter = App.Container.Resolve<IScriptInterpreter>();
            //if (!interpreter.Evaluate(script, value)) return Visibility.Collapsed;
            // TODO : Compile script once when created
            var test = interpreter.Evaluate(script, value);
            Console.WriteLine("The interpreter evaluates to {0}", test);
            var res = true ? Visibility.Visible : Visibility.Collapsed;
            counter++;
            return res;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
