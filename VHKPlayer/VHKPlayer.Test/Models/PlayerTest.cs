using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VHKPlayer.Interfaces;
using VHKPlayer.Models;
using System.IO;
using VHKPlayer.Test.Utility;
using VHKPlayer.ViewModels;
using VHKPlayer.Utility;

namespace VHKPlayer.Test.Models {
    /// <summary>
    /// Summary description for PlayerTest
    /// </summary>
    [TestClass]
    public class PlayerTest {
        IFolder _playerfolder;
        IPlayer _player;
        IFile _file;

        [TestInitialize]
        public void Setup() {
            var videoplayer = new VideoPlayer(new FolderSettings());
            _playerfolder = new FolderNode(Path.Combine(Constants.RootFolderPath, "spiller"));
            _file = new FileNode(Path.Combine(_playerfolder.FullPath, "001 - Chana de Souza Mason.png"));
            _player = new Player(_file);
        }

        [TestMethod]
        public void PlayerTestNameShouldBeChanaDeSouzaMason() {
            Assert.AreEqual("Chana de Souza Mason", _player.Name);
        }

        [TestMethod]
        public void PlayerTestNumberShouldBe1() {
            Assert.AreEqual(1, _player.Number);
        }

        [TestMethod]
        public void PlayerTestTrainerShouldBeFalse() {
            Assert.IsFalse(_player.Trainer);
        }

        [TestMethod]
        public void PlayerTestDalmoseTrainerShouldBeTrue() {
            IFile file = new FileNode(Path.Combine(Constants.RootFolderPath, "090 - Christian Dalmose.png"));
            IPlayer player = new Player(file);
            Assert.IsTrue(player.Trainer);
        }

        [TestMethod]
        public void PlayerTestRepeatShouldBeFalse() {
            Assert.IsFalse(_player.Repeat);
        }

        [TestMethod]
        public void PlayerTestContentCountShouldBe5() {
            Assert.AreEqual(5, _player.Content.Count);
        }

        [TestMethod]
        public void PlayerTestDalmoseContentCountShouldBe1() {
            IFile file = new FileNode(Path.Combine(Constants.RootFolderPath, "090 - Christian Dalmose.png"));
            IPlayer player = new Player(file);
            Assert.AreEqual(1, player.Content.Count);
        }
    }
}