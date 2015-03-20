using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VHKPlayer.Interfaces;
using VHKPlayer.Models;
using System.IO;
using VHKPlayer.Test.Utility;
using VHKPlayer.Facades;
using VHKPlayer.Utility;
using VHKPlayer.Strategies.Loading.Players;
using VHKPlayer.Enums;
using VHKPlayer.Strategies.Selection.Players;
using VHKPlayer.Exceptions;
using VHKPlayer.Factories.IPlayers;
using VHKPlayer.Strategies.Playing;

namespace VHKPlayer.Test.Models {
    /// <summary>
    /// Summary description for PlayerTest
    /// </summary>
    [TestClass]
    public class PlayerTest {
        IFolder _playerfolder;
        IPlayer _player, _dalmose;
        IFile _file;
        private Player _jackobsen;

        [TestInitialize]
        public void Setup() {
            var settings = new FolderSettings();
            settings["root"] = TestConstants.RootFolderPath;
            settings["statFolder"] = TestConstants.GithubPath + @"\DigiMatch";
            var videoplayer = new VideoPlayer(settings, new AlternatingPlayStrategy(new PlayFileStrategy(), new PlayPlayerStatStrategy(), new AutoPlayListPlayStrategy(), new DoNothingPlayStrategy()));
            _playerfolder = new FolderNode(Path.Combine(TestConstants.RootFolderPath, "spiller"));
            _file = new FileNode(Path.Combine(_playerfolder.FullPath, "001 - Chana de Souza Mason.png"));
            IFile file = new FileNode(Path.Combine(TestConstants.RootFolderPath, "090 - Christian Dalmose.png"));
            IFile jackobsenfile = new FileNode(Path.Combine(TestConstants.RootFolderPath, "012 - Astrid Jakobsen.png"));
            _player = new Player(new ViborgPlayerFactory(_file));
            _dalmose = new Player(new ViborgPlayerFactory(file));
            _jackobsen = new Player(new ViborgPlayerFactory(jackobsenfile));
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
        public void PlayerTestPlayCallOnPlayerWithPlayerStatTypeShouldResultInQueueWith3Files() {
            Assert.AreEqual(3, _player.Play(PlayType.PlayerStat).Count);
        }

        [TestMethod]
        public void PlayerTestPlayCallOnJackobsenWithPlayerStatTypeShouldResultInQueueWith2Files() {
            var queue = _jackobsen.Play(PlayType.PlayerStat);
            Assert.AreEqual(2, _jackobsen.Play(PlayType.PlayerStat).Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidTypeException), "Wrrong playtype for play call")]
        public void PlayerTestPlayCallWithVideoPlayTypeShouldResultInInvalidTypeException() {
            _player.Play(PlayType.Video);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidTypeException), "Wrrong playtype for play call")]
        public void PlayerTestPlayCallWithMusicPlayTypeShouldResultInInvalidTypeException() {
            _player.Play(PlayType.Music);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidTypeException), "Wrrong playtype for play call")]
        public void PlayerTestPlayCallWithPlayListPlayTypeShouldResultInInvalidTypeException() {
            _player.Play(PlayType.PlayList);
        }

        [TestMethod]
        public void PlayerTestIfPlayCallWithPlayTypePlayerStatDoesNotFindAVideoItShouldFindPlayerPicture() {
            var queue = _jackobsen.Play(PlayType.PlayerStat);
            var file = queue.Dequeue();
            Assert.IsTrue(Settings.PlayerPictureFolder.ContainsFile(file));
        }

        [TestMethod]
        public void PlayerTestDeSouzaStatsShoulddNotBeNull() {
            Assert.IsNotNull(_player.Stats);
        }

        [TestMethod]
        public void PlayerTestDeSouzaStatsSaveAttemptsShouldBe21() {
            Assert.AreEqual(21, _player.Stats.SaveAttempts);
        }

        [TestMethod]
        public void PlayerTestDeSouzaStatsSavesShouldBe7() {
            Assert.AreEqual(7, _player.Stats.Saves);
        }

        [TestMethod]
        public void PlayerTestDeSouzaStatsGoalsShouldBe0() {
            Assert.AreEqual(0, _player.Stats.Goals);
        }

        [TestMethod]
        public void PlayerTestDeSouzaStatsShotsShouldBe0() {
            Assert.AreEqual(0, _player.Stats.Goals);
        }

        [TestMethod]
        public void PlayerTestDeSouzaStatsYellowCardShouldBe0() {
            Assert.AreEqual(0, _player.Stats.YellowCard);
        }

        [TestMethod]
        public void PlayerTestDeSouzaStatsSuspensionShouldBe0() {
            Assert.AreEqual(0, _player.Stats.Suspension);
        }

        [TestMethod]
        public void PlayerTestDeSouzaStatsRedCardShouldBe0() {
            Assert.AreEqual(0, _player.Stats.RedCard);
        }

        [TestMethod]
        public void PlayerTestDeSouzaStatsCloneShouldBeEqualToOriginal() {
            Assert.IsTrue(_player.Stats.Equals(_player.Stats.Clone()));
        }
    }
}