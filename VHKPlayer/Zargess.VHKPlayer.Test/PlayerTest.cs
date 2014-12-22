using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zargess.VHKPlayer.Interfaces;
using System.IO;
using Zargess.VHKPlayer.Model;
using Zargess.VHKPlayer.Utility;
using Zargess.VHKPlayer.Factories.IPlayers;
using Zargess.VHKPlayer.Enums;

namespace Zargess.VHKPlayer.Test {
    /// <summary>
    /// Summary description for PlayerTest
    /// </summary>
    [TestClass]
    public class PlayerTest {
        IFolder _folder, _playerFolder;
        IFile _file;
        IPlayer _player;

        [TestInitialize()]
        public void Setup() {
            var path = @"D:\Github";
            if (Directory.Exists(path)) {
                Environment.CurrentDirectory = @"D:\GitHub";
                _folder = new FolderNode(@"D:\Dropbox\Programmering\C#\damer 2013-2014");
                App.ConfigService.Update("statsFolder", @"D:\Dropbox\Programmering\C#\DigiMatch");
            } else {
                Environment.CurrentDirectory = @"C:\Users\MFH\Documents\GitHub";
                App.ConfigService.Update("statsFolder", @"C:\Users\MFH\Dropbox\Programmering\C#\DigiMatch");
                _folder = new FolderNode(@"c:\users\mfh\vhk");
            }
            App.ConfigService.Update("root", _folder.FullPath);
            var root = App.ConfigService.GetString("root");
            _playerFolder = new FolderNode(PathHandler.CombinePaths(_folder.FullPath, "spiller"));
            _file = new FileNode(PathHandler.CombinePaths(_playerFolder.FullPath, @"\001 - Chana de Souza Mason.png"));
            _player = new Player(new PlayerFactory(_file));
        }

        [TestMethod]
        public void PlayerNumberShouldBe1() {
            Assert.AreEqual(1, _player.Number);
        }

        [TestMethod]
        public void PlayerNameShouldBeChana_de_Souza_Mason() {
            Assert.AreEqual("Chana de Souza Mason", _player.Name);
        }

        [TestMethod]
        public void PlayerSouldNotBeTrainer() {
            Assert.IsFalse(_player.Trainer);
        }

        [TestMethod]
        public void DalmoseShouldBeTrainer() {
            var file = new FileNode(PathHandler.CombinePaths(_playerFolder.FullPath, @"090 - Christian Dalmose.png"));
            var player = new Player(new PlayerFactory(file));
            Assert.IsTrue(player.Trainer);
        }

        [TestMethod]
        public void PlayerSizeShouldBe5() {
            Assert.AreEqual(5, _player.Content.Count);
        }

        [TestMethod]
        public void PlayerPlayCallWithStatParameterShouldReturnQueueOfSize3() {
            Assert.AreEqual(3, _player.Play(PlayType.PlayerStat).Count);
        }

        [TestMethod]
        public void PlayerPlayCallWithPicParameterShouldReturnQueueOfSize1() {
            Assert.AreEqual(1, _player.Play(PlayType.PlayerPic).Count);
        }

        [TestMethod]
        public void PlayerPlayCallWithVidParameterShouldReturnQueueOfSize1() {
            Assert.AreEqual(1, _player.Play(PlayType.PlayerVid).Count);
        }

        [TestMethod]
        public void PlayerPlayCallWithVidParameterShouldReturnAVideoFile() {
            var file = _player.Play(PlayType.PlayerVid).Dequeue();
            Assert.AreEqual(FileType.Video, file.Type);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Wrong PlayType for players!")]
        public void PlayerPlayCallWithPlayListParameterShouldReturnEmptyQueue() {
            var s = _player.Play(PlayType.PlayList).Count;
        }
    }
}
