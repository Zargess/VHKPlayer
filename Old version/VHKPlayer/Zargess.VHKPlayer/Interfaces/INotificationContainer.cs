using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.Interfaces {
    public interface INotificationContainer : INotifyPropertyChanged {
        ObservableCollection<INotification> Content { get; }
        void Add(INotification item);
        void Remove(INotification item);
    }
}