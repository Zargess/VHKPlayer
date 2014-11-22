using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Zargess.VHKPlayer.FileManagement.DataTypes;
using Zargess.VHKPlayer.FileManagement.EventHandlers;
using Zargess.VHKPlayer.FileManagement.Interfaces;
using Zargess.VHKPlayer.UtilFunctions;

namespace Zargess.VHKPlayer.FileManagement {
    public class Player : IPlayer {
        private ILoadingStrategy LoadingStrategy { get; set; }
        private List<IFile> Content { get; set; }
        public string Name { get; private set; }
        public bool Trainer { get; private set; }
        public int Number { get; private set; }
        public Statistics Stats { get; private set; }
        public event StatsChangedHandler StatsChanged;
        public int Size {
            get {
                return Content.Count;
            }
        }

        public Player(IFile picFile, ILoadingStrategy loadingStrategy) {
            Number = GetNumber(picFile);
            Name = GetName(picFile);
            Trainer = IsTrainer();
            Content = new List<IFile>();
            LoadingStrategy = loadingStrategy;
            LoadingStrategy.Load(Content);
        }

        private bool IsTrainer() {
            return Number >= 90;
        }

        private string GetName(IFile picFile) {
            var temp = picFile.Name.Remove(0, 6);
            temp = temp.Replace(".png", "");
            return temp;
        }

        private int GetNumber(IFile file) {
            var temp = file.Name.Substring(0, 3);
            return GeneralFunctions.StringToInteger(temp);
        }

        public ObservableCollection<IFile> GetContent() {
            var res = new ObservableCollection<IFile>();
            foreach (var file in Content) {
                res.Add(new FileNode(file.FullPath));
            }
            return res;
        }

        public Queue<IFile> Play(PlayType pt) {
            return null;
        }
    }
}