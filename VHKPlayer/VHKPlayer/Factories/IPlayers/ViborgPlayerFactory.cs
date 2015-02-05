using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Interfaces;
using VHKPlayer.Interfaces.Factories;
using VHKPlayer.Strategies.Loading.Players;
using VHKPlayer.Strategies.Loading.StatisticsLoading;
using VHKPlayer.Strategies.Peeking;
using VHKPlayer.Strategies.Selection.Players;
using VHKPlayer.Utility;

namespace VHKPlayer.Factories.IPlayers {
    public class ViborgPlayerFactory : IPlayerFactory {
        private IFile _file;

        public ViborgPlayerFactory(IFile file) {
            _file = file;
        }

        public ILoadingStrategy<IFile> CreateLoadingStrategy() {
            return new PlayerLoadingStrategy(_file);
        }

        public string CreateName() {
            return _file.NameWithoutExtension.Remove(0, 6);
        }

        public int CreateNumber() {
            return GeneralFunctions.StringToInteger(_file.Name.Substring(0, 3));
        }

        public List<IPlayerObserver> CreatePlayerObserverList() {
            return new List<IPlayerObserver>();
        }

        public bool CreateRepeat() {
            return false;
        }

        public IFileSelectionStrategy CreateSelectionStrategy() {
            return new TypeDependendSelectionStrategy(new PictureSelectionStrategy(), new VideoSelectionStrategy(), new StatSelectionStrategy(new PictureSelectionStrategy(), new NextInQueuePeekStrategy()));
        }

        public IStatsLoadingStrategy CreateStatsLoadingStrategy() {
            return new DigimatchStatsLoadingStrategy();
        }

        public bool CreateTrainer() {
            return CreateNumber() >= 90;
        }
    }
}
