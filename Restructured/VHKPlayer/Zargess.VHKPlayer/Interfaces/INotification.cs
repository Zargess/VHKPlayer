﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.Interfaces {
    public interface INotification : INotifyPropertyChanged {
        string Message { get; }
        bool Active { get; }
    }
}