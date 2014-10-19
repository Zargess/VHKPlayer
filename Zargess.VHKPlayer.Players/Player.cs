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
        private FileSystemWatcher StatWatcher { get; set; }
        // TODO : Consider making watchers for all of the filenodes here so that we can handle if the files are missing. Or if that makes no sense then make Exisits in filenode a method the playhandler can call.
        private FileSystemWatcher PictureWatcher { get; set; }  
        
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
            PictureWatcher = new FileSystemWatcher();
            if(!Trainer) InitStatWatcher();
            InitPictureWatcher();
        }

        private void InitPictureWatcher() {
            var path = Picture.FullPath.Replace(Picture.Name, "");
            var w = Utils.CreateWatcher(path, Picture.Name);
            w.Created += PictureWatcher_Created;
            w.Deleted += PictureWatcher_Created;
            w.EnableRaisingEvents = true;
            PictureWatcher = w;
        }

        private void PictureWatcher_Created(object sender, FileSystemEventArgs e) {
            switch (e.ChangeType) {
                case WatcherChangeTypes.Deleted:
                    var root = SettingsManager.GetSetting("root") as string;
                    if (String.IsNullOrEmpty(root)) return;
                    Picture = new FileNode(PathHandler.CombinePaths(root, "Logo.png"));
                    PictureChanged();
                    break;
                case WatcherChangeTypes.Created:
                    Picture = new FileNode(e.FullPath);
                    PictureChanged();
                    break;
            }
        }

        private void PictureChanged() {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs("Picture"));
            }
        }

        public void InitStatWatcher() {
            if (StatWatcher != null) return;
            while (true) {
                if (SettingsManager.GetSetting("statfolder") as string != "") {
                    var folder = new FolderNode(SettingsManager.GetSetting("statfolder") as string);
                    StatFile = new FileNode(PathHandler.CombinePaths(folder.FullPath, "VHK_" + Number + "player.xml"));
                    StatWatcher = Utils.CreateWatcher(folder.FullPath + "\\", StatFile.Name);
                    StatWatcher.Changed += OnChanged;
                    StatWatcher.Created += OnChanged;
                    StatWatcher.EnableRaisingEvents = true;
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
            if (StatWatcher == null) return;
            StatWatcher.EnableRaisingEvents = false;
            StatWatcher.Dispose();
            StatWatcher = null;
        }

        public override string ToString() {
            return Number + " - " + Name;
        }
    }
}