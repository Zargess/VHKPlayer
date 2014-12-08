﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.Model {
    public class Notification : INotification {
        public string Message { get; private set; }

        public Notification(string message) {
            Message = message;
        }
    }
}
