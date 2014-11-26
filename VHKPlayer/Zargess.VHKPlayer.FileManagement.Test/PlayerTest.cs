using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Zargess.VHKPlayer.UtilFunctions;
using Zargess.VHKPlayer.FileManagement.Interfaces;
using Zargess.VHKPlayer.SettingsManager;
using Zargess.VHKPlayer.FileManagement.Factories.Player;

namespace Zargess.VHKPlayer.FileManagement.Test {
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
            } else {
                Environment.CurrentDirectory = @"C:\Users\MFH\Documents\GitHub";
                SettingsManagement.Instance.SetSetting("statsFolder", @"C:\Users\MFH\Dropbox\Programmering\C#\DigiMatch");
                _folder = new FolderNode(@"c:\users\mfh\vhk");
            }
            SettingsManagement.Instance.SetSetting("root", _folder.FullPath);
            var root = SettingsManagement.Instance.GetStringSetting("root");
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
            Assert.AreEqual(5, _player.Size);
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
        public void PlayerCallWithVidParameterShouldReturnAVideoFile() {
            var file = _player.Play(PlayType.PlayerVid).Dequeue();
            Assert.AreEqual(FileType.Video, file.Type);
        }
    }
}