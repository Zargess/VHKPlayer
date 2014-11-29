using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.FileManagement.Interfaces {
    public interface IPlayerFactory {
        IFile CreateFile();
        string CreateName();
        int CreateNumber();
        bool CreateIfTrainer();
        ILoadingStrategy<IFile> CreateLoadingStrategy();
        IFileSelectionStrategy CreatePicSelectionStrategy();
        IFileSelectionStrategy CreateVidSelectionStrategy();
        IFileSelectionStrategy CreateStatSelectionStrategy();
        IStatsLoadingStrategy CreateStatsLoadingStrategy();
    }
}
