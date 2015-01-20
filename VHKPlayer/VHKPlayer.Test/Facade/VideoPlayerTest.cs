using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VHKPlayer.Interfaces;
using VHKPlayer.Utility;
using VHKPlayer.ViewModels;
using VHKPlayer.Test.TestClasses;
using VHKPlayer.Enums;
using VHKPlayer.Models;

namespace VHKPlayer.Test.Facade {
    /// <summary>
    /// Summary description for VideoPlayerTest
    /// </summary>
    [TestClass]
    public class VideoPlayerTest {
        private TestPlayController _observer;
        private IVideoPlayer _videoplayer;
        private IPlayable _testplayable;

        [TestInitialize]
        public void Setup() {
            _videoplayer = new VideoPlayer(new FolderSettings());
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
        public void AfterManagerCallsSetCurrentFileWithFoo_aviTestObserverVideoIsNotNull() {
            _videoplayer.Play(new FileNode("Foo.avi"));
            Assert.IsNotNull(_observer._video);
        }

        [TestMethod]
        public void AfterManagerCallsSetCurrentFileWithLogo_pngTestObserverPictureIsNotNull() {
            _videoplayer.Play(new FileNode("Logo.png"));
            Assert.IsNotNull(_observer._picture);
        }

        [TestMethod]
        public void AfterManagerCallsSetCurrentFileWithMuse_2_mp3TestObserverMusicIsNotNull() {
            _videoplayer.Play(new FileNode("muse_2.mp3"));
            Assert.IsNotNull(_observer._music);
        }

        [TestMethod]
        public void VideoPlayerTestPlayPlayableCallWithMusicPlayTypeShouldResultInEmptyQueue() {
            _videoplayer.PlayPlayable(_testplayable, PlayType.Music);
            Assert.AreEqual(0, _videoplayer.Queue.Count);
        }

        [TestMethod]
        public void VideoPlayerTestPlayPlayableCallWithVideoPlayTypeShouldResultInQueueWith2Elements() {
            _videoplayer.PlayPlayable(_testplayable, PlayType.Video);
            Assert.AreEqual(1, _videoplayer.Queue.Count);
        }
    }
}
