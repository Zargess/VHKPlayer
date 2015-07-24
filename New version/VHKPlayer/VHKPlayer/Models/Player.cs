using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Controllers.Interfaces;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Queries.GetPlayerStats;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility.LoadingStrategy.Interfaces;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

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
        public IQueryProcessor Processor { get; set; }

        public Player()
        {
            observers = new List<IVHKObserver<Player>>();
        }

        public void Play(IPlayStrategy strategy, IVideoPlayer videoPlayer)
        {
            strategy.Play(Content, videoPlayer);
        }

        public void SubjectUpdated(FolderNode subject)
        {
            Stats = Processor.Process(new GetPlayerStatsQuery()
            {
                Player = this
            });
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

        public override string ToString()
        {
            return Number + " - " + Name; 
        }
    }
}
