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
        public Action<object> Print { get; private set; }
        public ObservableCollection<PlayList> PlayLists { get; private set; } 

        public MainViewModel(Action<object> print) {
            Players = new ObservableCollection<Player>();
            People = new ObservableCollection<Player>();
            Audio = new ObservableCollection<FolderNode>();
            Video = new ObservableCollection<FolderNode>();
            PlayLists = new ObservableCollection<PlayList>();
            Print = print;
        }

        // TODO : Run some simulations on the loaded structure.
        public void LoadStructure(string path) {
            try {
                LoadFolders(path);
                LoadPlayLists(path);
                LoadPlayers(path);
            } catch (UnauthorizedAccessException e) {
                Print("You do not have permission to use this folder. \nPlease choose another one.\n" + e.Message);
            } catch (NullReferenceException e) {
                Print("You have not chosen a folder to load. Please do this before continuing.\n" + e.Message);
            }
        }

        public void LoadFolders(string path) {
            var root = new FolderNode(path);
            var limits = ((string)SettingsManager.GetSetting("limits")).Split(',').ToList();

            var folders = FolderLoading.getSomeFolders(root.FullPath, Utils.ToFSharpList(limits)).Select(x => new FolderNode(x)).ToList();
            folders.Where(x => x.Source == "musik").ToList().ForEach(Audio.Add);
            folders.Where(x => x.Source == root.Name && x.Name != "musik").ToList().ForEach(Video.Add);
            folders.ForEach(x => {
                foreach (var item in x.Files) {
                    Print(item.FullPath);
                }
            });
        }

        public void ReloadFolder(FolderNode fn) {
            fn.Refresh();
        }

        public void LoadPlayLists(string path) {
            var root = new FolderNode(path);
            try {
                PlayLists.Clear();
                var reks = PathHandler.CombinePaths(root.FullPath, "Rek");
                PlayLists.Add(new PlayList(PlaylistLoading.sortedPlaylist(reks, "RekFørKamp", 1)));
                PlayLists.Add(new PlayList(PlaylistLoading.sortedPlaylist(reks, "RekHalvej1", 2)));
                PlayLists.Add(new PlayList(PlaylistLoading.sortedPlaylist(reks, "RekHalvej2", 3)));
                PlayLists.Add(new PlayList(PlaylistLoading.sortedPlaylist(reks, "RekEfterKamp", 4)));
                PlayLists.Add(new SpecialList(PlaylistLoading.playlistFromFolderContent(PathHandler.CombinePaths(root.FullPath, "10sek"))));
                PlayLists.Add(new SpecialList(PlaylistLoading.playlistFromFolderContent(PathHandler.CombinePaths(root.FullPath, "ScorRek"))));
                PlayLists.Add(new SpecialList(PlaylistLoading.playlistFromFolderContent(PathHandler.CombinePaths(root.FullPath, "FoerKamp"))));
            } catch (UnauthorizedAccessException e) {
                Print("You do not have permission to use this folder. \nPlease choose another one.\n" + e.Message);
            } catch (NullReferenceException e) {
                Print("You have not chosen a folder to load. Please do this before continuing.\n" + e.Message);
            }
        }

        public void LoadPlayers(string path) {
            var root = new FolderNode(path);
            if (!root.ContainsFolder("Spiller") || !root.ContainsFolder("SpillerVideo") || !root.ContainsFolder("SpillerVideoStat")) return;
            try {
                var people = PlayerLoading.createAllPlayers(root.FullPath).ToList();
                Players.Clear();
                People.Clear();
                people.ForEach(x => People.Add(new Player(x)));
                people.Where(x => !x.Trainer).ToList().ForEach(x => Players.Add(new Player(x)));
                Players.ToList().ForEach(x => Print(x));
            } catch (UnauthorizedAccessException e) {
                Print("You do not have permission to use this folder. \nPlease choose another one.\n" + e.Message);
            } catch (NullReferenceException e) {
                Print("You have not chosen a folder to load. Please do this before continuing.\n" + e.Message);
            }
        }
    }
}