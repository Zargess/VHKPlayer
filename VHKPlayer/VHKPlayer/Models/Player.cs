using System.Collections.Generic;
using VHKPlayer.Controllers.Interfaces;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Queries.GetPlayerStats;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Models
{
    public class Player : IVhkObserver<FolderNode>, IPlayable
    {
        private List<IStatObserver> _observers;

        public string Name { get; set; }
        public int Number { get; set; }
        public bool Trainer { get; set; }
        public ICollection<FileNode> Content { get; set; }
        public Statistics Stats { get; private set; }
        public IQueryProcessor Processor { get; set; }

        public Player()
        {
            _observers = new List<IStatObserver>();
        }

        public void Play(IPlayStrategy strategy, IVideoPlayerController videoPlayer)
        {
            strategy.Play(Content, videoPlayer);
        }

        public void SubjectUpdated(FolderNode subject)
        {
            Stats = Processor.Process(new GetPlayerStatsQuery()
            {
                Player = this
            });
            _observers.ForEach(x => x.Notify(Stats));
        }

        public void AddObserver(IStatObserver observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IStatObserver observer)
        {
            _observers.Remove(observer);
        }

        public override string ToString()
        {
            var nr = "" + Number;

            if (Number < 10) nr = "00" + Number;
            else if (Number < 100) nr = "0" + Number;

            return nr + " - " + Name;
        }
    }
}