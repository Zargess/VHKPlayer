using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.NotificationManagement {
    public class NotificationManager : IEnumerable {
        public List<INotification> Notifycations { get; private set; }

        public NotificationManager() {
            Notifycations = new List<INotification>();
        }

        public void Add(INotification item) {
            Notifycations.Add(item);
            Console.WriteLine(item.Text);
        }

        public void Remove(INotification item) {
            Notifycations.Remove(item);
        }

        public IEnumerator GetEnumerator() {
            return Notifycations.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }
    }
}
