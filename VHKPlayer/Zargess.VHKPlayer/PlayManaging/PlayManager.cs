using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.EventHandlers;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.Model;
using Zargess.VHKPlayer.Utility;

namespace Zargess.VHKPlayer.PlayManaging {
    public class PlayManager : IPlayManager {
        public IPlayable CurrentPlayable { get; private set; }
        public Queue<IFile> Queue { get; private set; }
        private PlayType CurrentType { get; set; }
        public IPlayStrategy PlayStrategy { get; private set; }
        public IFileSelectionStrategy QueueEmptyStrategy { get; private set; }
        private List<IPlayObserver> Observers { get; set; }
        private IPlayList Auto10SekPlayList { get; set; }

        public PlayManager(IPlayStrategy playStrategy) {
            PlayStrategy = playStrategy;
            Observers = new List<IPlayObserver>();
            Auto10SekPlayList = GeneralFunctions.ConstructPlayList(App.ConfigService.GetString("auto10SekPlayList"));
        }

        public void PlayQueue() {
            if (Queue == null) return;
            if (CurrentPlayable.Repeat && Queue.Count == 0) {
                Queue = CurrentPlayable.Play(CurrentType);
            }
            var auto10sek = (bool)App.ConfigService.Get("auto10Sek");
            if (Queue.Count == 0 && auto10sek) {
                CurrentType = PlayType.PlayList;
                Queue = Auto10SekPlayList.Play(CurrentType);
            }
            if (Queue.Count == 0) return;
            var file = Queue.Dequeue();
            PlayStrategy.Play(file, CurrentType);
            if (file.Type != FileType.Music || CurrentPlayable.SelectionStrategy.HintNext(Queue, CurrentPlayable, CurrentType).Type == FileType.Music) return;
            PlayStrategy.Play(Queue.Dequeue(), CurrentType);
        }

        public void Play(IPlayable playable, PlayType type) {
            Queue = playable.Play(type);
            CurrentPlayable = playable;
            CurrentType = type;
            PlayQueue();
        }

        public void AddObserver(IPlayObserver observer) {
            Observers.Add(observer);
        }

        public void SetCurrentFile(IFile file) {
            Observers.ForEach(x => x.SetCurrentFile(file));
        }

        public void Play(FileType type) {
            Observers.ForEach(x => x.Play(type));
        }

        public void Pause(FileType type) {
            Observers.ForEach(x => x.Pause(type));
        }

        public void Stop(FileType type) {
            Observers.ForEach(x => x.Stop(type));
        }

        public void Mute(FileType type) {
            Observers.ForEach(x => x.Mute(type));
        }

        public void Resume(FileType type) {
            Observers.ForEach(x => x.Resume(type));
        }
    }
}