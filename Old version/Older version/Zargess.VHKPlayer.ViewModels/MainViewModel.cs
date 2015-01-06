﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Zargess.VHKPlayer.Collections;
using Zargess.VHKPlayer.FileManagement;
using Zargess.VHKPlayer.Players;
using Zargess.VHKPlayer.Utility;
using Zargess.VHKPlayer.LoadingPolicies;
using Zargess.VHKPlayer.Settings;

namespace Zargess.VHKPlayer.ViewModels {
    public class MainViewModel {
        public SortableCollection<Player> Players { get; private set; }
        public SortableCollection<Player> People { get; private set; }
        public SortableCollection<FolderNode> Audio { get; private set; }
        public SortableCollection<FolderNode> Video { get; private set; }
        public SortableCollection<PlayList> PlayLists { get; private set; }
        public List<FileSystemWatcher> Watchers { get; private set; } 

        public MainViewModel() {
            Players = new SortableCollection<Player>();
            People = new SortableCollection<Player>();
            Audio = new SortableCollection<FolderNode>();
            Video = new SortableCollection<FolderNode>();
            PlayLists = new SortableCollection<PlayList>();
        }

        // TODO : Move all loading methods to another class for a clean ViewModel
        public void LoadStructure(string path) {
            try {
                var root = new FolderNode(path);
                if (root.ValidRootFolder()) {
                    ClearData();
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

        public void LoadFolders(FolderNode root) {
            if (!root.ValidRootFolder()) {
                return;
            }
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
            Console.WriteLine("Folder done loading");
        }

        public void LoadPlayLists(FolderNode root) {
            PlayLists.Clear();
            var sortedTemp = SettingsManager.GetSetting("sortedPlayLists") as string;
            var specialTemp = SettingsManager.GetSetting("specialPlayLists") as string;
            if (!String.IsNullOrEmpty(sortedTemp)) {
                var temp = sortedTemp.Split(',');
                var reks = PathHandler.CombinePaths(root.FullPath, temp[0]);
                if (Directory.Exists(reks)) {
                    for (var i = 1; i < temp.Length; i++) {
                        var elements = temp[i].Split(';');
                        var name = elements[0].Replace("{", "");
                        var index = Utils.ConvertToInt(elements[1].Replace("}", ""));
                        PlayLists.Add(new SortedPlayList(PlaylistLoading.sortedPlaylist(reks, name, index), index, new FolderNode(reks)));
                    }
                }
            }

            if (!String.IsNullOrEmpty(specialTemp)) {
                var temp = specialTemp.Split(',');
                foreach (var s in temp) {
                    var elements = s.Split(';');
                    var name = elements[0].Replace("{", "");
                    var path = PathHandler.CombinePaths(root.FullPath,elements[1].Replace("}", ""));
                    if (Directory.Exists(path)) {
                        PlayLists.Add(new SpecialPlayList(PlaylistLoading.playlistFromFolderContent(name, path), new FolderNode(path)));
                    }
                }
            }
            Console.WriteLine("PlayList done loading");
        }

        // TODO : Consider making a Person class and make the Player class a subclass of Person. Then a trainer won't be a Player.
        public void LoadPlayers(FolderNode root) {
            if (!root.ContainsFolder("Spiller") || !root.ContainsFolder("SpillerVideo") || !root.ContainsFolder("SpillerVideoStat")) return;
            var people = PlayerLoading.createAllPlayers(root.FullPath).ToList();
            ClearIWatchable(People);
            ClearIWatchable(Players);
            foreach (var p in people.Select(person => new Player(person))) {
                People.Add(p);
                if (!p.Trainer) {
                    Players.Add(p);
                }
            }
            if (Watchers == null) {
                Watchers = new List<FileSystemWatcher>();
                var list =
                    new[] {"spiller", "spillervideo", "spillervideostat"}.Select(
                        x => Utils.CreateWatcher(PathHandler.CombinePaths(root.FullPath, x), "*.*"));
                foreach (var w in list) {
                    w.Created += (sender, args) => Application.Current.Dispatcher.Invoke(() => LoadPlayers(root));
                    w.Deleted += (sender, args) => Application.Current.Dispatcher.Invoke(() => LoadPlayers(root));
                    w.Renamed += (sender, args) => Application.Current.Dispatcher.Invoke(() => LoadPlayers(root));
                    w.Changed += (sender, args) => Application.Current.Dispatcher.Invoke(() => LoadPlayers(root));
                    w.EnableRaisingEvents = true;
                    Watchers.Add(w);
                }
            }
            Console.WriteLine("Players done loading");
        }

        private void ClearData() {
            ClearIWatchable(Audio);
            ClearIWatchable(Video);
            ClearIWatchable(People);
            ClearIWatchable(Players);
            ClearIWatchable(PlayLists);
        }

        private void ClearIWatchable<T>(ICollection<T> collection) where T : IWatchable {
            foreach (var element in collection) {
                element.StopListening();
            }
            collection.ClearOnUI();
        }
    }
}