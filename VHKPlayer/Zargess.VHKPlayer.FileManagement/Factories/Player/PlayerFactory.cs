using System;
using Zargess.VHKPlayer.FileManagement.Interfaces;
using Zargess.VHKPlayer.FileManagement.SharedInfo;
using Zargess.VHKPlayer.FileManagement.Strategies.Loading.IPlayer;
using Zargess.VHKPlayer.FileManagement.Strategies.Selection.IPlayer;
using Zargess.VHKPlayer.FileManagement.Strategies.StatsLoading;
using Zargess.VHKPlayer.UtilFunctions;

namespace Zargess.VHKPlayer.FileManagement.Factories.Player {
    public class PlayerFactory : IPlayerFactory {
        private IFile File { get; set; }

        public PlayerFactory(IFile file) {
            File = file;
        }

        public IFile CreateFile() {
            return new FileNode(File.FullPath);
        }

        public string CreateName() {
            var temp = File.Name.Remove(0, 6);
            temp = temp.Replace(".png", "");
            return temp;
        }

        public int CreateNumber() {
            var temp = File.Name.Substring(0, 3);
            return GeneralFunctions.StringToInteger(temp);
        }

        public bool CreateIfTrainer() {
            return CreateNumber() >= 90;
        }

        public ILoadingStrategy CreateLoadingStrategy() {
            return new PlayerLoadingStrategy(File);
        }

        public IFileSelectionStrategy CreatePicSelectionStrategy() {
            return new PictureSelectionStrategy();
        }

        public IFileSelectionStrategy CreateVidSelectionStrategy() {
            return new VideoSelectionStrategy(CreatePicSelectionStrategy());
        }

        public IFileSelectionStrategy CreateStatSelectionStrategy() {
            return new StatSelectionStrategy(CreatePicSelectionStrategy(), CreateVidSelectionStrategy());
        }

        public IStatsLoadingStrategy CreateStatsLoadingStrategy() {
            return new DigimatchStatsLoadingStrategy();
        }
    }
}