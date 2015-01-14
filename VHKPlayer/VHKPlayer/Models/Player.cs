using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Enums;
using VHKPlayer.Interfaces;
using VHKPlayer.Interfaces.Factories;
using VHKPlayer.Utility;

namespace VHKPlayer.Models {
    public class Player : IPlayer, IFolderObserver {
        private List<IPlayerObserver> _observers;
        private ILoadingStrategy<IFile> _loadingStrategy;
        private IFileSelectionStrategy _selectionStrategy;
        private IStatsLoadingStrategy _statsLoadingStrategy;

        public string Name { get; private set; }
        public int Number { get; private set; }
        public bool Trainer { get; private set; }
        public bool Repeat { get; private set; }
        public IStatistics Stats { get; private set; }
        public List<IFile> Content { get; private set; }

        public Player(IPlayerFactory factory) {
            Name = factory.CreateName();
            Number = factory.CreateNumber();
            Trainer = factory.CreateTrainer();
            Repeat = factory.CreateRepeat();
            _observers = factory.CreatePlayerObserverList();
            _loadingStrategy = factory.CreateLoadingStrategy();
            _selectionStrategy = factory.CreateSelectionStrategy();
            _statsLoadingStrategy = factory.CreateStatsLoadingStrategy();
            Content = LoadFiles();
            FolderChanged(null);
        }

        private List<IFile> LoadFiles() {
            return _loadingStrategy.Load(this);
        }

        public Queue<IFile> Play(PlayType type) {
            return _selectionStrategy.SelectFiles(this, type);
        }

        public void AddObserver(IPlayerObserver observer) {
            _observers.Add(observer);
        }

        public void RemoveObserver(IPlayerObserver observer) {
            _observers.Remove(observer);
        }

        public void FolderChanged(IFolder folder) {
            Stats = _statsLoadingStrategy.LoadStats(Number);
            _observers.ForEach(x => x.StatsChanged(Stats));
        }
    }
}