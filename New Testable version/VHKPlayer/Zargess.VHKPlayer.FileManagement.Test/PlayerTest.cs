using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Zargess.VHKPlayer.UtilFunctions;
using Zargess.VHKPlayer.FileManagement.Interfaces;
using Zargess.VHKPlayer.FileManagement.Strategies.Loading.IPlayer;
using Zargess.VHKPlayer.SettingsManager;

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
                _folder = new FolderNode(@"c:\users\mfh\vhk");
            }
            SettingsManagement.SetSetting("root", _folder.FullPath);
            var root = SettingsManagement.GetStringSetting("root");
            _playerFolder = new FolderNode(PathHandler.CombinePaths(_folder.FullPath, "spiller"));
            _file = new FileNode(PathHandler.CombinePaths(_playerFolder.FullPath, @"\001 - Chana de Souza Mason.png"));
            _player = new Player(_file, new PlayerLoadingStrategy(_file));
            var temp = _player.GetContent();
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
            var player = new Player(new FileNode(PathHandler.CombinePaths(_playerFolder.FullPath, @"090 - Christian Dalmose.png")), new PlayerLoadingStrategy(_file));
            Assert.IsTrue(player.Trainer);
        }

        [TestMethod]
        public void PlayerSizeShouldBe5() {
            Assert.AreEqual(5, _player.Size);
        }

        [TestMethod]
        public void PlayerPlayCallWithStatParameterShouldReturnQueueOfSize3() {
            Assert.AreEqual(3, _player.Play(PlayType.PlayerStat));
        }
    }
}