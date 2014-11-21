using System.Collections.Generic;
using System.Collections.ObjectModel;
using Zargess.VHKPlayer.SettingsManager;
using Zargess.VHKPlayer.FileManagement.Factories.PlayList;
using System;

namespace Zargess.VHKPlayer.FileManagement {
    public class PlayListContainer : IContainer {
        public ObservableCollection<IPlayable> Content { get; private set; }
        public string Name { get; private set; }

        public PlayListContainer() {
            Content = new ObservableCollection<IPlayable>();
            Name = "PlayLister";
        }

        public void Load() {
            Content.Clear();
            AddAll(AllFilesSorted());
            AddAll(IteratedFolder());
        }

        private IEnumerable<IPlayList> IteratedFolder() {
            var res = new List<IPlayList>();
            var strs = SettingsManagement.GetStringSetting("iteratedFolderPlayLists").Split(',');
            foreach (var s in strs) {
                res.Add(new PlayList(new IteratedFolderPlayListFactory(s)));
            }
            return res;
        }

        public IEnumerable<IPlayList> AllFilesSorted() {
            var res = new List<IPlayList>();
            var strs = SettingsManagement.GetStringSetting("allFilesSortedPlayLists").Split(',');
            foreach(var s in strs) {
                res.Add(new PlayList(new AllFilesSortedPlayListFactory(s)));
            }
            return res;
        }

        private void AddAll(IEnumerable<IPlayList> lists) {
            foreach(var p in lists) {
                Content.Add(p);
            }
        }
    }
}
