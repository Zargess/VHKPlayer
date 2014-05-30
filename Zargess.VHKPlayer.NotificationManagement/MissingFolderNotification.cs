using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.NotificationManagement {
    public class MissingFolderNotification : INotification {
        public string Text { get; set; }

        public MissingFolderNotification(string s) {
            Text = s;
        }
    }
}
