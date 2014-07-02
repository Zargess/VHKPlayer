using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Zargess.VHKPlayer.FileManagement;
using Zargess.VHKPlayer.Settings;

namespace Zargess.VHKPlayer.Players {
    public class Player {
        public int Number { get; set; }
        public FileNode StatPicture { get; set; }
        public FileNode StatMusic { get; set; }
        public FileNode StatVideo { get; set; }
        public FileNode Video { get; set; }
        public FileNode Picture { get; set; }
        public bool Keeper { get; set; }
        public bool Trainer { get; set; }
        public string Name { get; set; }
        public Statistics Stats { get; set; }
        public delegate void ValueChangedHandler(object sender, EventArgs e);
        public event ValueChangedHandler ValueChanged;
        public FileNode File { get; private set; }
        private FileSystemWatcher Watcher { get; set; }

        public Player(string name, int no, bool keeper) {
            Name = name;
            Number = no;
            Keeper = keeper;
            Trainer = Number >= 90;
            Stats = new Statistics();
            InitWatcher();
        }

        public Player(string name, int no, bool keeper, FileNode pic, FileNode vid, FileNode statvid, FileNode statmus, FileNode statpic) {
            Name = name;
            Number = no;
            Keeper = keeper;
            Picture = pic;
            Video = vid;
            StatVideo = statvid;
            StatMusic = statmus;
            StatPicture = statpic;
            Trainer = Number >= 90;
            Stats = new Statistics();
            InitWatcher();
        }

        private void InitWatcher() {
            while (true) {
                if (SettingsManager.GetSetting("statfolder") as string != "") {
                    var folder = new FolderNode(SettingsManager.GetSetting("statfolder") as string);
                    File = new FileNode(PathHandler.CombinePaths(folder.FullPath, "VHK_" + Number + "player.xml"));
                    Watcher = new FileSystemWatcher {
                        Path = folder.FullPath + "\\", Filter = File.Name, NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size
                    };
                    Watcher.Changed += OnChanged;
                    Watcher.Created += OnChanged;
                    Watcher.EnableRaisingEvents = true;
                } else {
                    var fbd = new FolderBrowserDialog {
                        Description = "Select the folder where the statistics is saved"
                    };
                    if (fbd.ShowDialog() == DialogResult.OK) {
                        SettingsManager.SetSetting("statfolder", fbd.SelectedPath);
                    }
                    continue;
                }
                break;
            }
        }

        private void OnChanged(object sender, FileSystemEventArgs e) {
            using (var reader = new XmlTextReader(File.FullPath)) {
                var old = new Statistics(Stats.Goals, Stats.Shots, Stats.Saves, Stats.SaveAttempts, Stats.YellowCard, Stats.Suspension, Stats.RedCard);
                while (reader.Read()) {
                    switch (reader.Name) {
                        case "shot":
                            Stats.Goals = ConvertToInt(reader.GetAttribute("goaltot"));
                            Stats.Shots = ConvertToInt(reader.GetAttribute("shottot"));
                            break;
                        case "penalty":
                            Stats.YellowCard = ConvertToInt(reader.GetAttribute("yellowcard"));
                            Stats.Suspension = ConvertToInt(reader.GetAttribute("suspension"));
                            Stats.RedCard = ConvertToInt(reader.GetAttribute("redcard"));
                            break;
                        case "mvshot":
                            Stats.Saves = ConvertToInt(reader.GetAttribute("savetot"));
                            Stats.SaveAttempts = ConvertToInt(reader.GetAttribute("shottot"));
                            break;
                    }
                }
                if (ValueChanged != null && !Stats.Equals(old)) {
                    ValueChanged(this, new EventArgs());
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