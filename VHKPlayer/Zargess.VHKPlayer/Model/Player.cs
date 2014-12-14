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
        private IFileSelectionStrategy PicSelection { get; set; }
        private IFileSelectionStrategy VidSelection { get; set; }
        private IFileSelectionStrategy StatSelection { get; set; }
        private IStatsLoadingStrategy StatsLoadingStrategy { get; set; }
        public ObservableCollection<IFile> Content { get; private set; }

        public string Name { get; private set; }
        public bool Trainer { get; private set; }
        public int Number { get; private set; }
        public Statistics Stats { get; private set; }
        public event StatsChangedHandler StatsChanged;

        public bool Repeat {
            get {
                return false;
            }

            set {}
        }

        public Player(IPlayerFactory factory) {
            Number = factory.CreateNumber();
            Name = factory.CreateName();
            Trainer = factory.CreateTrainer();
            Content = new ObservableCollection<IFile>();
            LoadingStrategy = factory.CreateLoadingStrategy();
            LoadingStrategy.Load(Content);
            PicSelection = factory.CreatePicSelectionStrategy();
            VidSelection = factory.CreateVidSelectionStrategy();
            StatSelection = factory.CreateStatSelectionStrategy();
            StatsLoadingStrategy = factory.CreateStatsLoadingStrategy();
            var statsFolder = new FolderNode(App.ConfigService.GetString("statsFolder"));
            statsFolder.FolderChanged += StatsFolderChanged;
            StatsFolderChanged(this, null);
        }

        public ObservableCollection<IFile> GetContent() {
            var res = new ObservableCollection<IFile>();
            foreach (var file in Content) {
                res.Add(file.Clone());
            }
            return res;
        }

        public Queue<IFile> Play(PlayType pt) {
            if (pt == PlayType.PlayerPic) return PicSelection.SelectFiles(this);
            if (pt == PlayType.PlayerVid) return VidSelection.SelectFiles(this);
            if (pt == PlayType.PlayerStat) return StatSelection.SelectFiles(this);
            return new Queue<IFile>();
        }

        private void StatsFolderChanged(object sender, EventArgs e) {
            Stats = StatsLoadingStrategy.LoadStats(Number);
            if (StatsChanged != null) {
                StatsChanged.Invoke(this, new StatEventArgs(Stats.Clone()));
            }
        }

        public override string ToString() {
            return Number + " " + Name;
        }
    }
}
