using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Utility.PlayStrategy.Interfaces;
using VHKPlayer.Utility.StatsLoading.Interfaces;

namespace VHKPlayer.Models
{
    public class Player : IVHKObserver<FolderNode>, IPlayable
    {
        private List<IVHKObserver<Player>> observers;

        public string Name { get; set; }
        public int Number { get; set; }
        public bool Trainer { get; set; }
        public ObservableCollection<FileNode> Content { get; set; }
        public Statistics Stats { get; private set; }
        public IStatsLoadingStrategy StatsLoadingStrategy { get; set; }

        public Player()
        {
            observers = new List<IVHKObserver<Player>>();
        }

        public void Play(IPlayStrategy strategy)
        {
            strategy.Play(Content);
        }

        public void SubjectUpdated(FolderNode subject)
        {
            Stats = StatsLoadingStrategy.LoadStats(Number);
            observers.ForEach(x => x.SubjectUpdated(this));
        }

        public void AddObserver(IVHKObserver<Player> observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IVHKObserver<Player> observer)
        {
            observers.Remove(observer);
        }
    }
}
