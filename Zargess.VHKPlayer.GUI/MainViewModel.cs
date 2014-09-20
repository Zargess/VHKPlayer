﻿using System;
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

        public MainViewModel(Action<object> print) {
            Players = new ObservableCollection<Player>();
            People = new ObservableCollection<Player>();
            Audio = new ObservableCollection<FolderNode>();
            Video = new ObservableCollection<FolderNode>();
            Print = print;
        }

        public void LoadStructure(string path) {
            try {
                var root = new FolderNode(path);

                var limits = ((string) SettingsManager.GetSetting("limits")).Split(',').ToList();

                var folders = FolderLoading.getSomeFolders(root.FullPath, Utils.ToFSharpList(limits)).Select(x => new FolderNode(x)).ToList();
                folders.ForEach(x => Print(x.FullPath));
                folders.Where(x => x.Source == "musik").ToList().ForEach(Audio.Add);
                folders.Where(x => x.Source == root.Name && x.Name != "musik").ToList().ForEach(Video.Add);

                var s = PlaylistLoading.rek;
                s.Content.ToList().ForEach(x => Print(x.Name));
                Print(s.Name);

                var b = PlaylistLoading.sek;
                b.Content.ToList().ForEach(x => Print(x.Name));
                Print(b.Name);

                LoadPlayers(path);
            } catch (UnauthorizedAccessException) {
                Print("You do not have permission to use this folder. \nPlease choose another one.");
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
            } catch (UnauthorizedAccessException) {
                Print("You do not have permission to use this folder. \nPlease choose another one.");
            }
        }
    }
}