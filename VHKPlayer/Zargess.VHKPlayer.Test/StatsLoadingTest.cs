using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zargess.VHKPlayer.Interfaces;
using System.IO;
using Zargess.VHKPlayer.Model;
using Zargess.VHKPlayer.Utility;
using Zargess.VHKPlayer.Factories.IPlayers;
using Zargess.VHKPlayer.Strategies.Loading.StatisticsLoading;

namespace Zargess.VHKPlayer.Test {
    /// <summary>
    /// Summary description for StatsLoadingTest
    /// </summary>
    [TestClass]
    public class StatsLoadingTest {
        IFolder _folder, _playerFolder;
        IFile _file;
        IPlayer _player;
        IStatsLoadingStrategy _digimatchStrategy;

        [TestInitialize()]
        public void Setup() {
            var path = @"D:\Github";
            if (Directory.Exists(path)) {
                Environment.CurrentDirectory = @"D:\GitHub";
                App.ConfigService.Update("statsFolder", @"D:\Github\VHKPlayer\DigiMatch");
                _folder = new FolderNode(@"D:\Dropbox\Programmering\C#\damer 2013-2014");
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
            _digimatchStrategy = new DigimatchStatsLoadingStrategy();
        }

        [TestMethod]
        public void Player1ShouldHave7Saves() {
            var stats = _digimatchStrategy.LoadStats(1);
            Assert.AreEqual(7, stats.Saves);
        }

        [TestMethod]
        public void Player1ShouldHave21SaveAttempts() {
            var stats = _digimatchStrategy.LoadStats(1);
            Assert.AreEqual(21, stats.SaveAttempts);
        }

        [TestMethod]
        public void Player1ClonedStatsShouldBeEqualToTheOriginal() {
            var stats = _digimatchStrategy.LoadStats(1);
            Assert.IsTrue(stats.Equals(stats.Clone()));
        }

        [TestMethod]
        public void Player1StatsHashCodeIsTheSameAsItsClone() {
            var stats = _digimatchStrategy.LoadStats(1);
            Assert.AreEqual(stats.GetHashCode(), stats.Clone().GetHashCode());
        }
    }
}
