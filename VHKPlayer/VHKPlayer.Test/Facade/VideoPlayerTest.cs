using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VHKPlayer.Interfaces;
using VHKPlayer.Utility;
using VHKPlayer.Facades;
using VHKPlayer.Test.TestClasses;
using VHKPlayer.Enums;
using VHKPlayer.Models;
using VHKPlayer.Strategies.Playing;
using VHKPlayer.Test.Utility;

namespace VHKPlayer.Test.Facade {
    /// <summary>
    /// Summary description for VideoPlayerTest
    /// </summary>
    [TestClass]
    public class VideoPlayerTests {
        private TestPlayController _observer;
        private IPlayable _testplayable;
        private IVideoPlayer _videoplayer;

        [TestInitialize]
        public void Setup() {
            var settings = new FolderSettings();
            settings["root"] = TestConstants.RootFolderPath;
            settings["statFolder"] = TestConstants.GithubPath + @"\DigiMatch";
            _videoplayer = new VideoPlayer(settings, new AlternatingPlayStrategy(new PlayFileStrategy(), new PlayPlayerStatStrategy(), new AutoPlayListPlayStrategy(), new DoNothingPlayStrategy()));
            _observer = new TestPlayController();
            _videoplayer.AddObserver(_observer);
            _testplayable = new TestPlayable();
        }

        [TestMethod]
        public void VideoPlayerTestAfterMuteCallTestControllerActionShouldBeMute() {
            _videoplayer.Mute(FileType.Music);
            Assert.AreEqual("mute", _observer._action);
        }

        [TestMethod]
        public void VideoPlayerTestAfterPauseCallTestControllerActionShouldBePause() {
            _videoplayer.Pause(FileType.Music);
            Assert.AreEqual("pause", _observer._action);
        }

        [TestMethod]
        public void VideoPlayerTestAfterPlayCallTestControllerActionShouldBePlay() {
            _videoplayer.Play(new FileNode(@"c:\test\temp.avi"));
            Assert.AreEqual("play", _observer._action);
        }

        [TestMethod]
        public void VideoPlayerTestAfterResumeCallTestControllerActionShouldBeResume() {
            _videoplayer.Resume(FileType.Music);
            Assert.AreEqual("resume", _observer._action);
        }

        [TestMethod]
        public void VideoPlayerTestAfterStopCallTestControllerActionShouldBeStop() {
            _videoplayer.Stop(FileType.Music);
            Assert.AreEqual("stop", _observer._action);
        }

        [TestMethod]
        public void VideoPlayerTestAfterManagerCallsSetCurrentFileWithFoo_aviTestObserverVideoIsNotNull() {
            _videoplayer.Play(new FileNode("Foo.avi"));
            Assert.IsNotNull(_observer._video);
        }

        [TestMethod]
        public void VideoPlayerTestAfterManagerCallsSetCurrentFileWithLogo_pngTestObserverPictureIsNotNull() {
            _videoplayer.Play(new FileNode("Logo.png"));
            Assert.IsNotNull(_observer._picture);
        }

        [TestMethod]
        public void VideoPlayerTestAfterManagerCallsSetCurrentFileWithMuse_2_mp3TestObserverMusicIsNotNull() {
            _videoplayer.Play(new FileNode("muse_2.mp3"));
            Assert.IsNotNull(_observer._music);
        }

        [TestMethod]
        public void VideoPlayerTestPlayPlayableCallWithMusicPlayTypeShouldResultInEmptyQueue() {
            _videoplayer.Play(_testplayable, PlayType.Music);
            Assert.AreEqual(0, _videoplayer.Queue.Count);
        }

        [TestMethod]
        public void VideoPlayerTestPlayPlayableCallWithVideoPlayTypeShouldResultInQueueWith0ElementsWhenFirstElementIsMusic() {
            _videoplayer.Play(_testplayable, PlayType.Video);
            Assert.AreEqual(0, _videoplayer.Queue.Count);
        }

        [TestMethod]
        public void VideoPlayerTestCallingPlayQueueWhenQueueHasOneElementShouldResultInAnEmptyQueue() {
            _videoplayer.Play(_testplayable, PlayType.Video);
            _videoplayer.PlayQueue();
            Assert.AreEqual(0, _videoplayer.Queue.Count);
        }

        [TestMethod]
        public void VideoPlayerTestCallingShowStatsWhenTestPlayerWasLastPlayableCalledShouldResultInTestControllerHavingAStatObjectWith2Scorings() {
            TestPlayController controller = new TestPlayController();
            _videoplayer.AddObserver(controller);
            _videoplayer.Play(new TestPlayer(), PlayType.PlayerPicture);
            _videoplayer.ShowStats();
            Assert.AreEqual(2, controller.Stats.Goals);
        }

        [TestMethod]
        public void VideoPlayerTestCallingPlayWithAPlayerAndPlayTypePlayerStatShouldResultInStatsShowingWith2Goals() {
            TestPlayController controller = new TestPlayController();
            _videoplayer.AddObserver(controller);
            _videoplayer.Play(new TestPlayer(), PlayType.PlayerStat);
            Assert.AreEqual(2, controller.Stats.Goals);
        }

        [TestMethod]
        public void VideoPlayerTestCallingPlayWithAPlayerAndPlayTypePlayerStatAndCurrentFileIsAPictureShouldResultInPlayerStatTimerIsEnabled() {
            _videoplayer.Play(new TestPlayer(), PlayType.PlayerStat);
            Assert.IsTrue(Settings.PlayerStatTimer.IsEnabled);
        }
        
        [TestMethod]
        public void VideoPlayerTestWhenAutoPlayListElementIsTrueTheAfterLastElementInQueueHasBeenPlayedTheFirstElementInTheAutoPlayListShouldBePlayedNext() {
            App.ViewModel.AutoPlayListEnabled = true;
            TestPlayController controller = new TestPlayController();
            _videoplayer.AddObserver(controller);
            _videoplayer.Play(new TestPlayer(), PlayType.PlayerVideo);
            _videoplayer.PlayQueue();
            Assert.AreEqual(Settings.AutoPlayList.Content[0], controller._video);
        }
    }
}