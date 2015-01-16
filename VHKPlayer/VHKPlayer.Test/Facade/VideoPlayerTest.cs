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


        [TestInitialize]
        public void Setup() {
            _videoplayer = new VideoPlayer(new FolderSettings());
            _observer = new TestPlayController();
            _videoplayer.AddObserver(_observer);
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
    }
}
