using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Collections;
using VHKPlayer.Enums;
using VHKPlayer.Interfaces;
using VHKPlayer.Interfaces.Factories;

namespace VHKPlayer.Models {
    public class PlayList : IPlayList {
        private IFolder _folder;
        private ILoadingStrategy<IFile> _loadingStrategy;
        private IFileSelectionStrategy _selectionStrategy;
        public ObservableCollection<IFile> Content { get; private set; }
        public bool HasAudio { get; private set; }
        public string Name { get; private set; }
        public bool Repeat { get; private set; }

        public PlayList(IPlayListFactory factory) {
            Content = new SortableCollection<IFile>();
            Name = factory.CreateName();
            HasAudio = factory.CreateHasAudio();
            Repeat = factory.CreateRepeat();
            _folder = factory.CreateFolder();
            _loadingStrategy = factory.CreateLoadingStrategy();
            _selectionStrategy = factory.CreateSelectionStrategy();
            _loadingStrategy.Load(Content);
            _folder.AddObserver(this);
        }

        public void FolderChanged(IFolder folder) {
            _loadingStrategy.Load(Content);
        }

        public Queue<IFile> Play(PlayType type) {
            return _selectionStrategy.SelectFiles(this, type);
        }

        public override string ToString() {
            return Name;
        }

        public IFile HintNext(Queue<IFile> queue) {
            return _selectionStrategy.HintNext(this, queue);
        }
    }
}