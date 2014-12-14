using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.Model {
    public class NotificationContainer : INotificationContainer {
        public ObservableCollection<INotification> Content { get; private set; }

        public bool HasActiveNotification {
            get {
                throw new NotImplementedException();
            }
        }

        public NotificationContainer() {
            Content = new ObservableCollection<INotification>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Add(INotification item) {
            Content.Add(item);
        }

        public void Remove(INotification item) {
            Content.Remove(item);
        }
    }
}
