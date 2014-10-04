using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.FileManagement;
using Zargess.VHKPlayer.Players;
using Zargess.VHKPlayer.Utility;
using Zargess.VHKPlayer.LoadingPolicies;
using Zargess.VHKPlayer.Settings;

namespace Zargess.VHKPlayer.GUI {
    public class MainViewModel {
        public ObservableCollection<Player> Players { get; private set; }
        public ObservableCollection<Player> People { get; private set; }
        public ObservableCollection<FolderNode> Audio { get; private set; }
        public ObservableCollection<FolderNode> Video { get; private set; }
        public ObservableCollection<PlayList> PlayLists { get; private set; }

        public MainViewModel() {
            Players = new ObservableCollection<Player>();
            People = new ObservableCollection<Player>();
            Audio = new ObservableCollection<FolderNode>();
            Video = new ObservableCollection<FolderNode>();
            PlayLists = new ObservableCollection<PlayList>();
        }

        public void LoadStructure(string path) {
            var root = new FolderNode(path);
            if (root.ValidRootFolder()) {
                ClearData();
                LoadFolders(root);
                LoadPlayLists(root);
                LoadPlayers(root);
            } else {
                Console.WriteLine("Could not load: {0}\nPlease choose another folder.", path);
            }
        }

        // TODO : Make this multithreaded
        public void LoadFolders(FolderNode root) {
            try {
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
            } catch (UnauthorizedAccessException e) {
                Console.WriteLine("You do not have permission to use this folder. \nPlease choose another one.\n" + e.Message);
            } catch (NullReferenceException e) {
                Console.WriteLine("You have not chosen a folder to load. Please do this before continuing.\n" + e.Message);
            }
        }

        public void ReloadFolder(FolderNode fn) {
            fn.Refresh();
        }

        public void LoadPlayLists(FolderNode root) {
            try {
                PlayLists.Clear();
                var reks = PathHandler.CombinePaths(root.FullPath, "rek");
                if (Directory.Exists(reks)) {
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
            } catch (UnauthorizedAccessException e) {
                Console.WriteLine("You do not have permission to use this folder. \nPlease choose another one.\n" + e.Message);
            } catch (NullReferenceException e) {
                Console.WriteLine("You have not chosen a folder to load. Please do this before continuing.\n" + e.Message);
            }
        }

        // TODO : Consider making a Person class and make the Player class a subclass of Person. Then a trainer won't be a Player.
        public void LoadPlayers(FolderNode root) {
            if (!root.ContainsFolder("Spiller") || !root.ContainsFolder("SpillerVideo") || !root.ContainsFolder("SpillerVideoStat")) return;
            try {
                var people = PlayerLoading.createAllPlayers(root.FullPath).ToList();
                ClearPeople(People);
                ClearPeople(Players);

                foreach (var p in people.Select(person => new Player(person))) {
                    People.Add(p);
                    if (!p.Trainer) {
                        Players.Add(p);
                    }
                }

                Players.ToList().ForEach(Console.WriteLine);
            } catch (UnauthorizedAccessException e) {
                Console.WriteLine("You do not have permission to use this folder. \nPlease choose another one.\n" + e.Message);
            } catch (NullReferenceException e) {
                Console.WriteLine("You have not chosen a folder to load. Please do this before continuing.\n" + e.Message);
            }
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