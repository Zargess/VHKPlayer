using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Zargess.VHKPlayer.Collections;
using Zargess.VHKPlayer.FileManagement;
using Zargess.VHKPlayer.Settings;
using Zargess.VHKPlayer.Players;

namespace Zargess.VHKPlayer.GUI.PlayManagement {
    public class PlayManager {
        public PlayerView PlayingView { get; private set; }
        public MediaElement Video { get; private set; }
        public MediaElement Audio { get; private set; }
        private Queue<FileNode> VideoQueue { get; set; }
        private FileNode LastPlayedVideoFile { get; set; }
        private FileNode LastPlayedMusicFile { get; set; }
        // TODO : Implement different play functions, a video and a audio queue
        public PlayManager(PlayerView pv) {
            PlayingView = pv;
            Video = PlayingView.Video;
            Audio = PlayingView.Audio;
            VideoQueue = new Queue<FileNode>();
        }

        public void Play(FileNode file) {
            switch (file.Type) {
                case FileType.Picture:
                    ShowImage(file);
                    break;
                case FileType.Music:
                    LastPlayedMusicFile = new FileNode(file.FullPath);
                    PlayFile(file, Audio);
                    break;
                case FileType.Video:
                    LastPlayedVideoFile = new FileNode(file.FullPath);
                    ShowVideoPlayer();
                    PlayFile(file, Video);
                    break;
            }
        }

        private void PlayFile(FileNode file, MediaElement me) {
            me.Source = new Uri(file.FullPath);
            me.Play();
        }

        public void Play(SortedPlayList list) {
            foreach (var file in list) {
                VideoQueue.Enqueue(file);
            }
            PlayQueue();
        }

        public void Play(SpecialPlayList list) {
            Play(list.GetNext());
        }

        public void Play(Player p, string type) {
            switch (type) {
                case "People" :
                    ShowImage(p.Picture);
                    break;
                case "Video" :
                    Play(p.Video);
                    break;
                case "Stat" :
                    PlayPlayerStat(p);
                    break;
            }
        }

        private void PlayPlayerStat(Player player) {
            // TODO : Implement the stat play sequence including the showing of stats on screen. Start by implementing as written to console
            throw new NotImplementedException();
        }

        public void PlayQueue() {

        }

        public void ShowImage(FileNode file) {
            if (file.Type != FileType.Picture) return;
            // TODO : Implement if the filetype is a ´picture
            HideVideo();
        }

        private void HideVideo() {
            Video.Visibility = Visibility.Hidden;
        }

        private void ShowVideoPlayer() {
            Video.Visibility = Visibility.Visible;
        }

        public void PostStatPlayList(SortableCollection<PlayList> list) {
            var name = SettingsManager.GetSetting("postStatPlayList") as string;
            if (String.IsNullOrEmpty(name)) return;
            var playlist = list.SingleOrDefault(x => x.Name == name) as SpecialPlayList;
            if (playlist == null) return;
            Play(playlist);
        }
    }
}
