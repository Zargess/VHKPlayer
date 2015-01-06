using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.Collections {
    public class CustomQueue<T> : Queue<T> {
        public void SetQueue(Queue<T> q) {
            for(var i = 0; i < Count; i++) {
                Dequeue();
            }
            foreach(T element in q) {
                Enqueue(element);
            }
        }
    }
}
