using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.FSharp.Collections;

namespace Zargess.VHKPlayer.Utility {
    public class Utils {
        public static FSharpList<T> ToFSharpList<T>(IList<T> input) {
            return CreateFSharpList(input, 0);
        }

        private static FSharpList<T> CreateFSharpList<T>(IList<T> input, int index) {
            if (index >= input.Count) {
                return FSharpList<T>.Empty;
            }
            return FSharpList<T>.Cons(input[index], CreateFSharpList(input, index + 1));
        }

        public static void TimeMethod<T>(Action<T> action, T input) {
            var watch = Stopwatch.StartNew();
            action(input);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Time used: {0}", elapsedMs);
        }

        public static void TimeMethod(Action action) {
            var watch = Stopwatch.StartNew();
            action();
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Time used: {0}", elapsedMs);
        }

        public static int ConvertToInt(string text) {
            int s;
            int.TryParse(text, out s);
            return s;
        }
    }
}
