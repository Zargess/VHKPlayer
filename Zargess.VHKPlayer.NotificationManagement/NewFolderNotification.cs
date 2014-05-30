using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.NotificationManagement {
    public class NewFolderNotification : INotification {
        public string Text { get; set; }

        public NewFolderNotification(string s) {
            Text = s;
        }
    }
}
