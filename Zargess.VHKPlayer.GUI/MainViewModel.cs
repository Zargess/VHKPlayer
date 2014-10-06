using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Collections;
using Zargess.VHKPlayer.FileManagement;
using Zargess.VHKPlayer.Players;
using Zargess.VHKPlayer.Utility;
using Zargess.VHKPlayer.LoadingPolicies;
using Zargess.VHKPlayer.Settings;

namespace Zargess.VHKPlayer.GUI {
    public class MainViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableSafeCollection<Player> Players { get; private set; }
        public ObservableSafeCollection<Player> People { get; private set; }
        public ObservableSafeCollection<FolderNode> Audio { get; private set; }
        public ObservableSafeCollection<FolderNode> Video { get; private set; }
        public ObservableSafeCollection<PlayList> PlayLists { get; private set; }
        private bool _foldershowable;
        private bool _playlistshowable;
        private bool _peopleshowable;
        public bool FolderShowable {
            get { return _foldershowable; }
            private set {
                _foldershowable = value;
                if (PropertyChanged != null) {
                    PropertyChanged(this,new PropertyChangedEventArgs("_foldershowable"));
                }
            }
        }
        public bool PlayListShowable {
            get { return _playlistshowable; }
            set {
                _playlistshowable = value;
                if (PropertyChanged != null) {
                    PropertyChanged(this, new PropertyChangedEventArgs("_playlistshowable"));
                }
            }
        }
        public bool PeopleShowable {
            get { return _playlistshowable; }
            set {
                _peopleshowable = value;
                if (PropertyChanged != null) {
                    PropertyChanged(this, new PropertyChangedEventArgs("_peopleshowable"));
                }
            }
        }

        public MainViewModel() {
            Players = new ObservableSafeCollection<Player>();
            People = new ObservableSafeCollection<Player>();
            Audio = new ObservableSafeCollection<FolderNode>();
            Video = new ObservableSafeCollection<FolderNode>();
            PlayLists = new ObservableSafeCollection<PlayList>();
            FolderShowable = false;
            PlayListShowable = false;
            PeopleShowable = false;
        }

        public void LoadStructure(string path) {
            try {
                var root = new FolderNode(path);
                if (root.ValidRootFolder()) {
                    Utils.TimeMethod(ClearData);
                    Utils.TimeMethod(LoadFolders, root);
                    Utils.TimeMethod(LoadPlayLists, root);
                    Utils.TimeMethod(LoadPlayers, root);
                } else {
                    Console.WriteLine("Could not load: {0}\nPlease choose another folder.", path);
                }
            } catch (UnauthorizedAccessException e) {
                Console.WriteLine("You do not have permission to use this folder. \nPlease choose another one.\n" + e.Message);
            } catch (NullReferenceException e) {
                Console.WriteLine("You have not chosen a folder to load. Please do this before continuing.\n" + e.Message);
            }
        }

        public void LoadStructureThreaded(string path) {
            try {
                var root = new FolderNode(path);
                if (root.ValidRootFolder()) {
                    ClearData();
                    new Thread(() => LoadFolders(root)).Start();
                    new Thread(() => LoadPlayLists(root)).Start();
                    new Thread(() => LoadPlayers(root)).Start();
                } else {
                    Console.WriteLine("Could not load: {0}\nPlease choose another folder.", path);
                }
            } catch (UnauthorizedAccessException e) {
                Console.WriteLine("You do not have permission to use this folder. \nPlease choose another one.\n" + e.Message);
            } catch (NullReferenceException e) {
                Console.WriteLine("You have not chosen a folder to load. Please do this before continuing.\n" + e.Message);
            }
        }

        public void LoadFolders(FolderNode root) {
            if (!root.ValidRootFolder()) {
                return;
            }
            FolderShowable = false;
            ClearFolderList(Video);
            ClearFolderList(Audio);
            var limits = ((string)SettingsManager.GetSetting("limits")).Split(',').ToList();

            var folders = FolderLoading.getSomeFolders(root.FullPath, Utils.ToFSharpList(limits)).Select(x => new FolderNode(x)).ToList();

            foreach (var folder in folders) {
                if (folder.Source == "musik") {
                    Audio.Add(folder);
                    folder.InitWatcher();
                } else if (folder.Source == root.Name && folder.Name != "musik") {
                    Video.Add(folder);
                    folder.InitWatcher();
                }
            }
            FolderShowable = true;
            Console.WriteLine("Folder done loading");
        }

        public void ReloadFolder(FolderNode fn) {
            fn.Refresh();
        }

        public void LoadPlayLists(FolderNode root) {
            var reks = PathHandler.CombinePaths(root.FullPath, "rek");
            if (Directory.Exists(reks)) {
                PlayListShowable = false;
                PlayLists.Clear();
                PlayLists.Add(new PlayList(PlaylistLoading.sortedPlaylist(reks, "RekFørKamp", 1)));
                PlayLists.Add(new PlayList(PlaylistLoading.sortedPlaylist(reks, "RekHalvej1", 2)));
                PlayLists.Add(new PlayList(PlaylistLoading.sortedPlaylist(reks, "RekHalvej2", 3)));
                PlayLists.Add(new PlayList(PlaylistLoading.sortedPlaylist(reks, "RekEfterKamp", 4)));
                PlayLists.Add(
                    new SpecialList(
                        PlaylistLoading.playlistFromFolderContent(PathHandler.CombinePaths(root.FullPath, "10sek"))));
                // TODO : Consider making a special playlist type for ScorRek
                PlayLists.Add(
                    new SpecialList(
                        PlaylistLoading.playlistFromFolderContent(PathHandler.CombinePaths(root.FullPath, "ScorRek"))));
                PlayLists.Add(
                    new SpecialList(
                        PlaylistLoading.playlistFromFolderContent(PathHandler.CombinePaths(root.FullPath, "FoerKamp"))));
            } else {
                Console.WriteLine("The folder {0} does not exists. Please select a proper root folder", reks);
            }
            PlayListShowable = true;
            Console.WriteLine("PlayList done loading");
        }

        // TODO : Consider making a Person class and make the Player class a subclass of Person. Then a trainer won't be a Player.
        public void LoadPlayers(FolderNode root) {
            if (!root.ContainsFolder("Spiller") || !root.ContainsFolder("SpillerVideo") || !root.ContainsFolder("SpillerVideoStat")) return;
            PeopleShowable = false;
            var people = PlayerLoading.createAllPlayers(root.FullPath).ToList();
            ClearPeople(People);
            ClearPeople(Players);

            foreach (var p in people.Select(person => new Player(person))) {
                People.Add(p);
                if (!p.Trainer) {
                    Players.Add(p);
                }
            }

            //Players.ToList().ForEach(Console.WriteLine);
            PeopleShowable = true;
            Console.WriteLine("Players done loading");
        }

        private void ClearData() {
            ClearFolderList(Audio);
            ClearFolderList(Video);
            ClearPeople(People);
            ClearPeople(Players);
            ClearPlayLists(PlayLists);
        }

        private void ClearFolderList(ICollection<FolderNode> list) {
            foreach (var folder in list) {
                folder.StopListening();
            }
            list.Clear();
        }

        private void ClearPeople(ICollection<Player> list) {
            foreach (var player in list) {
                player.StopListener();
            }
            list.Clear();
        }

        private void ClearPlayLists(ICollection<PlayList> list) {
            list.Clear();
        }
    }
}