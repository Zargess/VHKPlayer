using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Zargess.VHKPlayer.FileManagement;

namespace Zargess.VHKPlayer.Players {
    public class Statistics {
        public delegate void ValueChangedHandler(object sender, EventArgs e);
        public event ValueChangedHandler ValueChanged;
        private int Number { get; set; }
        public FolderNode Folder { get; private set; }
        public FileNode File { get; private set; }
        private FileSystemWatcher Watcher { get; set; }
        public int Goals { get; private set; }
        public int Shots { get; private set; }
        public int Saves { get; private set; }
        public int SaveAttempts { get; private set; }
        public int YellowCard { get; private set; }
        public int Suspension { get; private set; }
        public int RedCard { get; private set; }

        public Statistics(FolderNode f, int no) {
            Folder = f;
            Number = no;
            File = new FileNode(PathHandler.CombinePaths(Folder.FullPath, "VHK_" + Number + "player.xml"));
            Watcher = new FileSystemWatcher {
                Path = Folder.FullPath + "\\",
                Filter = File.Name,
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size
            };
            Watcher.Changed += OnChanged;
            Watcher.Created += OnChanged;
            Watcher.EnableRaisingEvents = true;
            Goals = 0;
            Shots = 0;
            Saves = 0;
            SaveAttempts = 0;
            YellowCard = 0;
            Suspension = 0;
            RedCard = 0;
        }

        public void Stop() {
            Watcher.EnableRaisingEvents = false;
        }

        public void Start() {
            Watcher.EnableRaisingEvents = true;
        }

        private void OnChanged(object sender, FileSystemEventArgs e) {
            using (var reader = new XmlTextReader(File.FullPath)) {
                while (reader.Read()) {
                    switch (reader.Name) {
                        case "shot":
                            Goals = ConvertToInt(reader.GetAttribute("goaltot"));
                            Shots = ConvertToInt(reader.GetAttribute("shottot"));
                            break;
                        case "penalty":
                            YellowCard = ConvertToInt(reader.GetAttribute("yellowcard"));
                            Suspension = ConvertToInt(reader.GetAttribute("suspension"));
                            RedCard = ConvertToInt(reader.GetAttribute("redcard"));
                            break;
                        case "mvshot":
                            Saves = ConvertToInt(reader.GetAttribute("savetot"));
                            SaveAttempts = ConvertToInt(reader.GetAttribute("shottot"));
                            break;
                    }
                }
                if (ValueChanged != null) {
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