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
using VHKPlayer.Strategies.Loading.Players;
using VHKPlayer.Enums;
using VHKPlayer.Strategies.Selection.Players;

namespace VHKPlayer.Test.Models {
    /// <summary>
    /// Summary description for PlayerTest
    /// </summary>
    [TestClass]
    public class PlayerTest {
        IFolder _playerfolder;
        IPlayer _player, _dalmose;
        IFile _file;

        [TestInitialize]
        public void Setup() {
            var videoplayer = new VideoPlayer(new FolderSettings());
            _playerfolder = new FolderNode(Path.Combine(Constants.RootFolderPath, "spiller"));
            _file = new FileNode(Path.Combine(_playerfolder.FullPath, "001 - Chana de Souza Mason.png"));
            IFile file = new FileNode(Path.Combine(Constants.RootFolderPath, "090 - Christian Dalmose.png"));
            _player = new Player(_file, new PlayerLoadingStrategy(_file), new TypeDependendSelectionStrategy(new PictureSelectionStrategy(), new VideoSelectionStrategy()));
            _dalmose = new Player(file, new PlayerLoadingStrategy(file), new TypeDependendSelectionStrategy(new PictureSelectionStrategy(), new VideoSelectionStrategy()));
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
            Assert.IsTrue(_dalmose.Trainer);
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
            Assert.AreEqual(1, _dalmose.Content.Count);
        }

        [TestMethod]
        public void PlayerTestPlayCallOnPlayerWithPlayerPictureTypeShouldResultInAQueueWith1File() {
            Assert.AreEqual(1, _player.Play(PlayType.PlayerPicture).Count);
        }

        [TestMethod]
        public void PlayerTestPlayCallOnPlayerWithPlayerVideoTypeShouldResultInQueueWith1File() {
            Assert.AreEqual(1, _player.Play(PlayType.PlayerVideo).Count);
        }

        [TestMethod]
        public void PlayerTestResultOfPlayCallOnPlayerWithPlayerVideoTypeShouldBeContainedInPlayerVideoFolder() {
            var queue = _player.Play(PlayType.PlayerVideo);
            var file = queue.Dequeue();
            Assert.IsTrue(Settings.PlayerVideoFolder.ContainsFile(file));
        }

        [TestMethod]
        public void PlayerTestPlayCallOnPlayerWithPlayerStatTypeShouldResultInQueueWith3File() {
            Assert.AreEqual(3, _player.Play(PlayType.PlayerStat).Count);
        }
    }
}