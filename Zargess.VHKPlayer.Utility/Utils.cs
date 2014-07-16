using System;
using System.Collections.Generic;
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
    }
}
