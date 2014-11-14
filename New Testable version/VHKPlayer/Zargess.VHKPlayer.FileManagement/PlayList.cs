using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.FileManagement.Test;

namespace Zargess.VHKPlayer.FileManagement {
    public class PlayList : IPlayList {
        private IFileSelectionStrategy SelectionStrategy { get; set; }

        public string Name { get; private set; }
        public ObservableCollection<IFile> Content { get; private set; }

        public PlayList(string name, IFileSelectionStrategy selectionStrategy) {
            Name = name;
            Content = new ObservableCollection<IFile>();
            SelectionStrategy = selectionStrategy;
        }

        public void Add(IFile file) {
            Content.Add(file);
        }

        public Queue<IFile> Play() {
            return SelectionStrategy.SelectFiles(this);
        }

        public void InitWatcher() {
            throw new NotImplementedException();
        }

        public void StopWatcher() {
            throw new NotImplementedException();
        }
    }
}