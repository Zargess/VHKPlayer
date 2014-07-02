using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.FileManagement;
using Zargess.VHKPlayer.Players;
using Zargess.VHKPlayer.Settings;

namespace Zargess.VHKPlayer.GUI {
    public class MainViewModel {
        public List<Player> Players { get; private set; }
        public List<FileNode> Files { get; set; }
        public ObservableCollection<FolderNode> Audio { get; private set; }
        public ObservableCollection<FolderNode> Video { get; private set; }
        public CommandPrompt Cmd { get; set; }

        public MainViewModel(CommandPrompt cmd) {
            Players = new List<Player>();
            Files = new List<FileNode>();
            Audio = new ObservableCollection<FolderNode>();
            Video = new ObservableCollection<FolderNode>();
            Cmd = cmd;
        }

        public void LoadStructure(string path) {
            var root = new FolderNode(SettingsManager.GetSetting("root") as string);
            var audiofolder = new FolderNode(PathHandler.CombinePaths(root.FullPath, "musik"));
            var folders = Directory.GetDirectories(path, "*", SearchOption.AllDirectories);

            foreach (var folder in folders.Where(x => x != "").Select(f => new FolderNode(f))) {
                if (folder.FullPath.Contains(audiofolder.FullPath) && folder.Source == "musik") {
                    Files.AddRange(folder.Files.Select(x => new FileNode(x.FullPath)));
                    Audio.Add(folder);
                } else if (folder.FullPath.Contains(root.FullPath) && folder.Name != "musik") {
                    Video.Add(folder);
                    if (!folder.Name.Contains("Spiller") || folder.Name == "SpillerUd") continue;
                    foreach (var file in folder.Files.Where(x => x.Type != FileType.Unsupported)) {
                        var no = ConvertToInt(file.Name.Substring(1, 2));
                        var name = file.Name.Substring(6, file.Name.Length - 10);
                        var p = Players.SingleOrDefault(x => x.Number == no);
                        if (p == null) {
                            p = new Player(name, no, false);
                            Players.Add(p);
                        }
                        switch (file.Source) {
                            case "Spiller":
                                p.Picture = new FileNode(file.FullPath);
                                break;
                            case "SpillerVideo":
                                p.Video = new FileNode(file.FullPath);
                                break;
                            case "SpillerVideoStat":
                                p.StatPicture = new FileNode(file.FullPath);
                                p.StatVideo = new FileNode(PathHandler.CombinePaths(folder.FullPath, "Video\\" + file.NameWithNoExtension + ".avi"));
                                p.StatMusic = new FileNode(PathHandler.CombinePaths(file.FullPath, "mp3\\" + file.NameWithNoExtension + ".mp3"));
                                break;
                        }
                    }
                }
            }
        }

        private int ConvertToInt(string text) {
            int s;
            int.TryParse(text, out s);
            return s;
        }
    }
}
