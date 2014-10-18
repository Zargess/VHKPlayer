using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Zargess.VHKPlayer.FileManagement;
using Zargess.VHKPlayer.LoadingPolicies;
using Zargess.VHKPlayer.Settings;
using Zargess.VHKPlayer.Utility;

namespace Zargess.VHKPlayer.Players {
    public class Player : INotifyPropertyChanged{
        public delegate void ValueChangedHandler(object sender, EventArgs e);
        public event ValueChangedHandler ValueChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public int Number { get; set; }
        public bool Trainer { get; set; }
        public string Name { get; set; }
        public FileNode StatPicture { get; set; }
        public FileNode StatMusic { get; set; }
        public FileNode StatVideo { get; set; }
        public FileNode Video { get; set; }
        public FileNode Picture { get; set; }
        public Statistics Stats { get; set; }
        public FileNode StatFile { get; private set; }
        private FileSystemWatcher Watcher { get; set; }
        // TODO : Consider making watchers for all of the filenodes here so that we can handle if the files are missing. Or if that makes no sense then make Exisits in filenode a method the playhandler can call.
        private Dictionary<string,FileSystemWatcher> Watchers { get; set; }  
        
        public Player(PlayerLoading.Player player) {
            Name = player.Name;
            Number = player.Number;
            Trainer = player.Trainer;
            Video = new FileNode(player.Video.Path);
            Picture = new FileNode(player.Picture.Path);
            var staFiles = player.StatFiles;
            StatPicture = new FileNode(staFiles.Picture.Path);
            StatVideo = new FileNode(staFiles.Video.Path);
            StatMusic = new FileNode(staFiles.Music.Path);
            Stats = new Statistics();
            Watchers = new Dictionary<string, FileSystemWatcher>();
            if(!Trainer) InitWatcher();
            InitWatchers();
        }

        private void InitWatchers() {
            var files = new List<FileNode> {
                StatPicture,StatMusic,StatVideo,Video,Picture
            };
            foreach (var file in files) {
                var path = file.FullPath.Replace(file.Name, "");
                var w = Utils.CreateWatcher(path, file.Name);
                // TODO : Complete these watchers
                Watchers.Add(file.Name, w);
            }
        }

        public void InitWatcher() {
            if (Watcher != null) return;
            while (true) {
                if (SettingsManager.GetSetting("statfolder") as string != "") {
                    var folder = new FolderNode(SettingsManager.GetSetting("statfolder") as string);
                    StatFile = new FileNode(PathHandler.CombinePaths(folder.FullPath, "VHK_" + Number + "player.xml"));
                    Watcher = Utils.CreateWatcher(folder.FullPath + "\\", StatFile.Name);
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
            if (StatFile.Exists()) {
                OnChanged(null, null);
            }
        }

        private void OnChanged(object sender, FileSystemEventArgs e) {
            using (var reader = new XmlTextReader(StatFile.FullPath)) {
                var old = Stats.Clone();
                while (reader.Read()) {
                    switch (reader.Name) {
                        case "shot":
                            Stats.Goals = Utils.ConvertToInt(reader.GetAttribute("goaltot"));
                            Stats.Shots = Utils.ConvertToInt(reader.GetAttribute("shottot"));
                            break;
                        case "penalty":
                            Stats.YellowCard = Utils.ConvertToInt(reader.GetAttribute("yellowcard"));
                            Stats.Suspension = Utils.ConvertToInt(reader.GetAttribute("suspension"));
                            Stats.RedCard = Utils.ConvertToInt(reader.GetAttribute("redcard"));
                            break;
                        case "mvshot":
                            Stats.Saves = Utils.ConvertToInt(reader.GetAttribute("savetot"));
                            Stats.SaveAttempts = Utils.ConvertToInt(reader.GetAttribute("shottot"));
                            break;
                    }
                }
                if (ValueChanged != null && !Stats.Equals(old)) {
                    ValueChanged(this, new EventArgs());
                }
            }
        }

        public void StopListener() {
            if (Watcher == null) return;
            Watcher.EnableRaisingEvents = false;
            Watcher.Dispose();
            Watcher = null;
        }

        public override string ToString() {
            return Number + " - " + Name;
        }
    }
}