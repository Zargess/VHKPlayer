using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Enums;
using VHKPlayer.Interfaces;
using VHKPlayer.Models;
using VHKPlayer.Test.Utility;

namespace VHKPlayer.Test.TestClasses {
    public class TestPlayer : IPlayer {
        public ObservableCollection<IFile> Content {
            get {
                throw new NotImplementedException();
            }
        }

        public string Name {
            get {
                throw new NotImplementedException();
            }
        }

        public int Number {
            get {
                throw new NotImplementedException();
            }
        }

        public bool Repeat {
            get {
                throw new NotImplementedException();
            }
        }

        public IStatistics Stats {
            get {
                var stats = new Statistics();
                stats.Goals = 2;
                return stats;
            }
        }

        public bool Trainer {
            get {
                throw new NotImplementedException();
            }
        }

        public void AddObserver(IPlayerObserver observer) {
            throw new NotImplementedException();
        }

        public Queue<IFile> Play(PlayType type) {
            var queue = new Queue<IFile>();
            queue.Enqueue(new FileNode(Path.Combine(Constants.RootFolderPath, @"spillervideostat\002 - Rikke Skov.png")));
            return queue;
        }

        public void RemoveObserver(IPlayerObserver observer) {
            throw new NotImplementedException();
        }
    }
}
