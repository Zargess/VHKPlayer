using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Zargess.VHKPlayer.FileManagement.DataTypes;
using Zargess.VHKPlayer.FileManagement.EventHandlers;
using Zargess.VHKPlayer.FileManagement.Interfaces;

namespace Zargess.VHKPlayer.FileManagement {
    public class Player : IPlayer {
        private ILoadingStrategy LoadingStrategy { get; set; }
        public string Name { get; private set; }
        public int Size { get; private set; }
        public bool Trainer { get; private set; }
        public int Number { get; private set; }
        public Statistics Stats { get; private set; }

        public event StatsChangedHandler StatsChanged;

        public Player() {

        }

        public ObservableCollection<IFile> GetContent() {
            throw new NotImplementedException();
        }

        public Queue<IFile> Play(PlayType pt) {
            throw new NotImplementedException();
        }
    }
}