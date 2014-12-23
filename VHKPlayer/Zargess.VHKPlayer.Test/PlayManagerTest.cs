using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zargess.VHKPlayer.Interfaces;
using System.IO;
using Zargess.VHKPlayer.Utility;
using Zargess.VHKPlayer.Test.TestClasses;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.Model;

namespace Zargess.VHKPlayer.Test {
    /// <summary>
    /// Summary description for PlayManagerTest
    /// </summary>
    [TestClass]
    public class PlayManagerTest {
        TestObserver _observer;
        [TestInitialize]
        public void Setup() {
            Constants.GetRootFolder();
            _observer = new TestObserver();
            App.PlayManager.AddObserver(_observer);
        }

        [TestMethod]
        public void AfterMangerCallsMuteTestObserverActionShouldBeMute() {
            App.PlayManager.Mute(FileType.Picture);
            Assert.AreEqual("mute", _observer._action);
        }

        [TestMethod]
        public void AfterManagerCallsPauseTestObserverActionShouldBePause() {
            App.PlayManager.Pause(FileType.Picture);
            Assert.AreEqual("pause", _observer._action);
        }

        [TestMethod]
        public void AfterManagerCallsPlayTestObserverActionShouldBePlay() {
            App.PlayManager.Play(FileType.Picture);
            Assert.AreEqual("play", _observer._action);
        }

        [TestMethod]
        public void AfterManagerCallsResumeTestObserverActionShouldBeResume() {
            App.PlayManager.Resume(FileType.Picture);
            Assert.AreEqual("resume", _observer._action);
        }

        [TestMethod]
        public void AfterManagerCallsStopTestObserverActionShouldBeStop() {
            App.PlayManager.Stop(FileType.Picture);
            Assert.AreEqual("stop", _observer._action);
        }

        [TestMethod]
        public void AfterManagerCallsSetCurrentFileWithFoo_aviTestObserverVideoIsNotNull() {
            App.PlayManager.SetCurrentFile(new FileNode("Foo.avi"));
            Assert.IsNotNull(_observer._video);
        }

        [TestMethod]
        public void AfterManagerCallsSetCurrentFileWithLogo_pngTestObserverPictureIsNotNull() {
            App.PlayManager.SetCurrentFile(new FileNode("Logo.png"));
            Assert.IsNotNull(_observer._picture);
        }

        [TestMethod]
        public void AfterManagerCallsSetCurrentFileWithMuse_2_mp3TestObserverMusicIsNotNull() {
            App.PlayManager.SetCurrentFile(new FileNode("muse_2.mp3"));
            Assert.IsNotNull(_observer._music);
        }

        [TestMethod]
        public void AfterManagerCallsPlayWithAuto10SekQueueCountShouldBe17() {
            App.PlayManager.Play(App.PlayManager.Auto10SekPlayList, PlayType.PlayList);
            Assert.AreEqual(0, App.PlayManager.Queue.Count);
        }
    }
}
