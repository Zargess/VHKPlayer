using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHKPlayer.Collections {
    public class CustomQueue<T> : Queue<T> {
        public void SetQueue(IEnumerable<T> collection) {
            Clear();

            foreach (var item in collection) {
                Enqueue(item);
            }
        } 
    }
}
