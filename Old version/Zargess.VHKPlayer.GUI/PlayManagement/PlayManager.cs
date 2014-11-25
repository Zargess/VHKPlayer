using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Zargess.VHKPlayer.Collections;
using Zargess.VHKPlayer.FileManagement;
using Zargess.VHKPlayer.Settings;
using Zargess.VHKPlayer.Players;
using Timer = System.Windows.Forms.Timer;

namespace Zargess.VHKPlayer.GUI.PlayManagement {
    public class PlayManager {
        public PlayerView PlayingView { get; private set; }
        public MediaElement Video { get; private set; }
        public MediaElement Audio { get; private set; }
        private Queue<FileNode> VideoQueue { get; set; }
        private FileNode LastPlayedVideoFile { get; set; }
        private FileNode LastPlayedMusicFile { get; set; }
        private Dictionary<string, MediaElement> MediaElements { get; set; }

        // TODO : Implement different play functions, a video and a audio queue
        public PlayManager(PlayerView pv) {
            PlayingView = pv;
            Video = PlayingView.Video;
            Audio = PlayingView.Audio;
            VideoQueue = new Queue<FileNode>();
            MediaElements = new Dictionary<string, MediaElement> { { "video", Video }, { "audio", Audio } };
            Video.MediaEnded += (sender, args) => PlayQueue();
        }

        public void Play(FileNode file) {
            switch (file.Type) {
                case FileType.Picture:
                    ShowImage(file, null);
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
            Console.WriteLine("Playing " + file.Name);
        }

        private void PlayFile(FileNode file, MediaElement me) {
            me.Source = new Uri(file.FullPath);
            //me.Volume = 10.0;
            me.Play();
        }

        public void Play(PlayList list) {
            VideoQueue.Clear();
            foreach (var file in list.GetContent()) {
                VideoQueue.Enqueue(new FileNode(file.FullPath));
            }
            PlayQueue();
        }

        public void Play(Player p, string type) {
            switch (type) {
                case "people":
                    ShowImage(p.Picture, p);
                    break;
                case "video":
                    Play(p.Video);
                    break;
                case "stat":
                    PlayPlayerStat(p);
                    break;
            }
            Console.WriteLine("play {0} {1}", type, p.Name);
        }

        private void PlayPlayerStat(Player player) {
            // TODO : Implement the stat play sequence including the showing of stats on screen. Start by implementing as written to console

        }

        public void PlayQueue() {
            Console.WriteLine("PlayQueue is not done yet.... :(");

            if (VideoQueue.Count > 0) {
                Play(VideoQueue.Dequeue());
            }
        }

        public void ShowImage(FileNode file, Player p) {
            if (file.Type != FileType.Picture) return;
            HideVideoPlayer();
            PlayingView.Background = new ImageBrush(new BitmapImage(new Uri(file.FullPath)));
            if (file.Source == "SpillerVideoStat") {
                ShowStats(p);
            }
        }

        private void ShowStats(Player player) {
            player.ValueChanged += ValuesUpdated;
            UpdateLabelText(player.Stats);
            var t = new Timer {Interval = 5000};
            t.Tick +=
                (sender, args) =>
                    Application.Current.Dispatcher.Invoke(() => {
                        player.ValueChanged -= ValuesUpdated;
                        var q = sender as Timer;
                        q.Stop();
                        t.Enabled = false;
                    });
            t.Enabled = true;
            t.Start();
        }

        private void ValuesUpdated(object sender, StatEventArgs args) {
            UpdateLabelText(args.Stats);
        }

        private void UpdateLabelText(Statistics stats) {
            PlayingView.Penalties.Content = stats.Suspension;
            ChangeLabelText(PlayingView.Saves, stats.Saves, stats.SaveAttempts);
            ChangeLabelText(PlayingView.Scorings, stats.Goals, stats.Shots);
            ShowLabels(stats.Shots > 0, stats.Suspension > 0, stats.SaveAttempts > 0);
        }

        private void ChangeLabelText(Label l, int succesfull, int attempts) {
            l.Content = succesfull + "/" + attempts;
        }

        private void ShowLabels(bool shots, bool saves, bool suspensions) {
            PlayingView.Scorings.Visibility = GetVisibility(shots);
            PlayingView.Penalties.Visibility = GetVisibility(suspensions);
            PlayingView.Saves.Visibility = GetVisibility(saves);
        }

        private Visibility GetVisibility(bool b) {
            return b ? Visibility.Visible : Visibility.Hidden;
        }

        private void HideVideoPlayer() {
            Video.Visibility = Visibility.Hidden;
        }

        private void ShowVideoPlayer() {
            Video.Visibility = Visibility.Visible;
        }

        public void PostStatPlayList(SortableCollection<PlayList> list) {
            var name = SettingsManager.GetSetting("postStatPlayList") as string;
            if (String.IsNullOrEmpty(name)) return;
            var playlist = list.SingleOrDefault(x => x.Name == name);
            if (playlist == null) return;
            Play(playlist);
        }

        public void Stop(string me) {
            if (!MediaElements.ContainsKey(me)) return;
            MediaElements[me].Stop();
        }

        public void Pause(string me) {
            if (!MediaElements.ContainsKey(me)) return;
            MediaElements[me].Pause();
        }

        public void Continue(string me) {
            if (!MediaElements.ContainsKey(me)) return;
            MediaElements[me].Play();
        }
    }
}