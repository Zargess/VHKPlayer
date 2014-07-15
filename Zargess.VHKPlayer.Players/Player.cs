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
        public delegate void ValueChangedHandler(object sender, EventArgs e);
        public event ValueChangedHandler ValueChanged;

        public int Number { get; set; }
        public readonly Func<bool> Trainer;
        public string Name { get; set; }

        public FileNode StatPicture { get; set; }
        public FileNode StatMusic { get; set; }
        public FileNode StatVideo { get; set; }
        public FileNode Video { get; set; }
        public FileNode Picture { get; set; }
        public Statistics Stats { get; set; }
        public FileNode StatFile { get; private set; }
        private FileSystemWatcher Watcher { get; set; }

        public Player(string name, int no) {
            Name = name;
            Number = no;
            Trainer = () => {
                var res = Number >= 90 && Picture != null && Video == null && StatVideo == null && StatPicture == null &&
                          StatMusic == null;
                if (!res) {
                    InitWatcher();
                }
                return res;
            };
            Stats = new Statistics();
            InitWatcher();
        }

        private void InitWatcher() {
            while (true) {
                if (SettingsManager.GetSetting("statfolder") as string != "") {
                    var folder = new FolderNode(SettingsManager.GetSetting("statfolder") as string);
                    StatFile = new FileNode(PathHandler.CombinePaths(folder.FullPath, "VHK_" + Number + "player.xml"));
                    Watcher = new FileSystemWatcher {
                        Path = folder.FullPath + "\\", Filter = StatFile.Name, NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size
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
            if (StatFile.Exists) {
                OnChanged(null, null);
            }
        }

        private void OnChanged(object sender, FileSystemEventArgs e) {
            using (var reader = new XmlTextReader(StatFile.FullPath)) {
                var old = Stats.Clone();
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