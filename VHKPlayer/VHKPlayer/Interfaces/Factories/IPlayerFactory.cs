using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHKPlayer.Interfaces.Factories {
    public interface IPlayerFactory {
        string CreateName();
        int CreateNumber();
        bool CreateTrainer();
        bool CreateRepeat();
        List<IPlayerObserver> CreatePlayerObserverList();
        ILoadingStrategy<IFile> CreateLoadingStrategy();
        IFileSelectionStrategy CreateSelectionStrategy();
        IStatsLoadingStrategy CreateStatsLoadingStrategy();
    }
}
