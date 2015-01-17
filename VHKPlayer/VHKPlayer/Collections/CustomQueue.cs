using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHKPlayer.Collections {
    public class CustomQueue<T> : Queue<T> {
        public void SetQueue(IEnumerable<T> collection) {
            for(var i = 0; i < Count; i++) {
                Dequeue();
            }

            foreach (var item in collection) {
                Enqueue(item);
            }
        } 
    }
}
