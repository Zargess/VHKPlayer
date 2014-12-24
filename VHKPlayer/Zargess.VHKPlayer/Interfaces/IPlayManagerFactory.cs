﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Collections;

namespace Zargess.VHKPlayer.Interfaces {
    public interface IPlayManagerFactory {
        CustomQueue<IFile> CreateQueue();
        IPlayStrategy CreatePlayStrategy();
        IFileSelectionStrategy CreateQueueEmptyStrategy();
        IPlayList CreateAuto10SekPlayList();
        List<IPlayObserver> CreateObserverList();
    }
}
