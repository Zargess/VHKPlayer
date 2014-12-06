using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.Interfaces {
    public interface IVideoPlayerFactory {
        IContainer<IContainer<IPlayable>> CreateMusicContainer();
        IContainer<IPlayable> CreatePlayerContainer();
        IContainer<IPlayable> CreatePlayListContainer();
        IContainer<IPlayable> CreateCardContainer();
        IContainer<IPlayable> CreateMiscContainer();
        IContainer<IPlayable> CreatePlayerOut();
    }
}