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

namespace Zargess.VHKPlayer.GUI {
    public class MainViewModel {
        public List<PlayerLoading.Player> Players { get; private set; }
        public List<PlayerLoading.Player> Trainers { get; private set; }
        public ObservableCollection<FolderNode> Audio { get; private set; }
        public ObservableCollection<FolderNode> Video { get; private set; }
        public Action<string> Print { get; private set; }

        public MainViewModel(Action<string> print) {
            Players = new List<PlayerLoading.Player>();
            Trainers = new List<PlayerLoading.Player>();
            Audio = new ObservableCollection<FolderNode>();
            Video = new ObservableCollection<FolderNode>();
            Print = print;
        }

        public void LoadStructure(string path) {
            try {
                var root = new FolderNode(path);

                var limits = new List<string> {
                    "Temp", 
                    "Arkiv - Sct Mathias Centret - Benyttes igen efter", 
                    "Originaler", 
                    "Normalized", 
                    "Arkiv", 
                    "konverteret", 
                    "Spiller", 
                    "SpillerVideo", 
                    "spillerVideoStat"
                };

                var folders = FolderLoading.getSomeFolders(root.FullPath, Utils.ToFSharpList(limits)).Select(x => new FolderNode(x)).ToList();
                folders.ForEach(x => Print(x.FullPath));

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
            if (!root.ContainsFolder("Spiller")) return;
            try {
                var people = PlayerLoading.createAllPlayers(root.FullPath).ToList();
                Players = people.Where(x => !x.Trainer).ToList();
                Trainers = people.Where(x => x.Trainer).ToList();
            } catch (UnauthorizedAccessException) {
                Print("You do not have permission to use this folder. \nPlease choose another one.");
            }
        }
    }
}