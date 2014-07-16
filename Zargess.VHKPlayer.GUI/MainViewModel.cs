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
        public List<Player> Players { get; private set; }
        public ObservableCollection<FolderNode> Audio { get; private set; }
        public ObservableCollection<FolderNode> Video { get; private set; }
        public Action<string> Print { get; private set; }

        public MainViewModel(Action<string> print) {
            Players = new List<Player>();
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

                if (!root.ContainsFolder("Spiller")) return;
                var people = PlayerLoading.createAllPlayers(root.FullPath).ToList();
                var players = people.Where(x => !x.Trainer).ToList();
                players.ForEach(x => Print("Number: " + x.Number + ", Name: " + x.Name));
                #region old algorithem
                //var root = new FolderNode(SettingsManager.GetSetting("root") as string);
                //var audiofolder = new FolderNode(PathHandler.CombinePaths(root.FullPath, "musik"));
                //foreach (var folder in folders.Select(f => new FolderNode(f))) {
                //    if (folder.FullPath.Contains(audiofolder.FullPath) && folder.Source == "musik") {
                //        Audio.Add(folder);
                //    } else if (folder.FullPath.Contains(root.FullPath) && !folder.FullPath.Contains(audiofolder.FullPath)) {
                //        Video.Add(folder);
                //        if (!folder.Name.Contains("Spiller") || folder.Name == "SpillerUd") continue;
                //        foreach (var file in folder.Files.Where(x => x.Type != FileType.Unsupported)) {
                //            var no = ConvertToInt(file.Name.Substring(1, 2));
                //            var name = file.Name.Substring(6, file.Name.Length - 10);
                //            var p = Players.SingleOrDefault(x => x.Number == no);
                //            if (p == null) {
                //                p = new Player(name, no);
                //                Players.Add(p);
                //            }
                //            switch (file.Source) {
                //                case "Spiller":
                //                    p.Picture = new FileNode(file.FullPath);
                //                    break;
                //                case "SpillerVideo":
                //                    p.Video = new FileNode(file.FullPath);
                //                    break;
                //                case "SpillerVideoStat":
                //                    p.StatPicture = new FileNode(file.FullPath);
                //                    p.StatVideo = new FileNode(PathHandler.CombinePaths(folder.FullPath, "Video\\" + file.NameWithNoExtension + ".avi"));
                //                    p.StatMusic = new FileNode(PathHandler.CombinePaths(file.FullPath, "mp3\\" + file.NameWithNoExtension + ".mp3"));
                //                    break;
                //            }
                //        }
                //    } else if (folder.Name == "musik") {
                //        LoadStructure(folder.FullPath);
                //    }
                //}
                #endregion
            } catch (UnauthorizedAccessException) {
                Print("You do not have permission to use this folder. \nPlease choose another one.");
            }
        }

        //private int ConvertToInt(string text) {
        //    int s;
        //    int.TryParse(text, out s);
        //    return s;
        //}
    }
}