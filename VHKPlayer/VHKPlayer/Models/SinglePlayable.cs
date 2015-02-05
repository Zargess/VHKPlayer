using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using VHKPlayer.Enums;
using VHKPlayer.Interfaces;

namespace VHKPlayer.Models {
    public class SinglePlayable : IPlayable {
        public string Name { get; private set; }
        public bool Repeat { get; private set; }
        public ObservableCollection<IFile> Content { get; private set; }

        public SinglePlayable(IFile file) {
            Name = file.Name;
            Repeat = false;
            Content = new ObservableCollection<IFile>();
            Content.Add(file);
        }

        public Queue<IFile> Play(PlayType type) {
            var queue = new Queue<IFile>();
            queue.Enqueue(Content[0]);
            return queue;
        }

        public override string ToString() {
            return Name;
        }

        public IFile HintNext(Queue<IFile> queue) {
            return null;
        }
    }
}