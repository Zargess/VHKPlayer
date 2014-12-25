using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.EventHandlers;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.Model {
    public class Player : IPlayer {
        private ILoadingStrategy<IFile> LoadingStrategy { get; set; }
        private IStatsLoadingStrategy StatsLoadingStrategy { get; set; }
        public ObservableCollection<IFile> Content { get; private set; }
        public IFileSelectionStrategy SelectionStrategy { get; private set; }

        public string Name { get; private set; }
        public bool Trainer { get; private set; }
        public int Number { get; private set; }
        public Statistics Stats { get; private set; }

        public bool Repeat {
            get {
                return false;
            }

            set {}
        }

        public List<IPlayerObserver> Observers { get; private set; }

        public Player(IPlayerFactory factory) {
            Number = factory.CreateNumber();
            Name = factory.CreateName();
            Trainer = factory.CreateTrainer();
            Content = new ObservableCollection<IFile>();
            Observers = new List<IPlayerObserver>();
            LoadingStrategy = factory.CreateLoadingStrategy();
            LoadingStrategy.Load(Content);
            StatsLoadingStrategy = factory.CreateStatsLoadingStrategy();
            SelectionStrategy = factory.CreateSelectionStrategy();
            var statsFolder = new FolderNode(App.ConfigService.GetString("statsFolder"));
            statsFolder.FolderChanged += StatsFolderChanged;
            StatsFolderChanged(this, null);
        }

        public ObservableCollection<IFile> GetContent() {
            var res = new ObservableCollection<IFile>();
            foreach (var file in Content) {
                res.Add(file);
            }
            return res;
        }

        public Queue<IFile> Play(PlayType pt) {
            return SelectionStrategy.SelectFiles(this, pt);
        }

        private void StatsFolderChanged(object sender, EventArgs e) {
            Stats = StatsLoadingStrategy.LoadStats(Number);
            Observers.ForEach(x => x.StatsChanged(Stats.Clone()));
        }

        public override string ToString() {
            return Number + " " + Name;
        }

        public void AddObserver(IPlayerObserver observer) {
            Observers.Add(observer);
        }

        public void RemoveObserver(IPlayerObserver observer) {
            Observers.Remove(observer);
        }
    }
}
