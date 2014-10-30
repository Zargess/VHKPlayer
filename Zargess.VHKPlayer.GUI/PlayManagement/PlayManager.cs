using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Zargess.VHKPlayer.Collections;
using Zargess.VHKPlayer.FileManagement;
using Zargess.VHKPlayer.Settings;

namespace Zargess.VHKPlayer.GUI.PlayManagement {
    public class PlayManager {
        public PlayerView PlayingView { get; private set; }
        public MediaElement Video { get; private set; }
        public MediaElement Audio { get; private set; }
        private Queue<FileNode> VideoQueue { get; set; }
        private FileNode LastPlayedVideoFile { get; set; }
        // TODO : Implement different play functions, a video and a audio queue
        public PlayManager(PlayerView pv) {
            PlayingView = pv;
            Video = PlayingView.Video;
            Audio = PlayingView.Audio;
            VideoQueue = new Queue<FileNode>();
        }

        public void Play(FileNode file) {
            
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

        public void PlayQueue() {
            
        }

        public void ShowImage(FileNode file) {
            if (file.Type != FileType.Picture) return;
            // TODO : Implement if the filetype is a ´picture
        }

        private void HideVideo() {
            Video.Visibility = Visibility.Hidden;
        }

        private void ShowVideo() {
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
