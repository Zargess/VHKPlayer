using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VHKPlayer.ViewModels;
using VHKPlayer.Utility;
using VHKPlayer.Interfaces;
using VHKPlayer.Models;
using System.IO;
using VHKPlayer.Test.Utility;
using VHKPlayer.Factories.IPlayLists;
using VHKPlayer.Enums;
using VHKPlayer.Strategies.Playing;

namespace VHKPlayer.Test.Models {
    /// <summary>
    /// Summary description for PlayListTest
    /// </summary>
    [TestClass]
    public class PlayListTest {
        private IPlayList _playlist;
        private FolderNode _folder;

        [TestInitialize]
        public void Setup() {
            var settings = new FolderSettings();
            settings["root"] = TestConstants.RootFolderPath;
            var videoplayer = new VideoPlayer(settings, new AlternatingPlayStrategy(new PlayFileStrategy(), new PlayPlayerStatStrategy(), new AutoPlayListPlayStrategy()));
            _folder = new FolderNode(Path.Combine(TestConstants.RootFolderPath, "10sek"));
            _playlist = new PlayList(new AllFilesSortedPlayListFactory("{10sek;" + _folder.FullPath + ";2;true;true;AllFilesSorted}"));
        }

        [TestMethod]
        public void PlayListTestNameShouldBe10Sek() {
            Assert.AreEqual("10sek", _playlist.Name);
        }

        [TestMethod]
        public void PlayListTestHasAudioShouldBeTrue() {
            Assert.IsTrue(_playlist.HasAudio);
        }

        [TestMethod]
        public void PlayListTestHasAudioShouldBeFalse() {
            _playlist = new PlayList(new AllFilesSortedPlayListFactory("{10sek;" + _folder.FullPath + ";2;false;false;AllFilesSorted}"));
            Assert.IsFalse(_playlist.HasAudio);
        }

        [TestMethod]
        public void PlayListTestRepeatShouldBeFalse() {
            _playlist = new PlayList(new AllFilesSortedPlayListFactory("{10sek;" + _folder.FullPath + ";2;false;false;AllFilesSorted}"));
            Assert.IsFalse(_playlist.Repeat);
        }

        [TestMethod]
        public void PlayListTestRepeatShouldBeTrue() {
            Assert.IsTrue(_playlist.Repeat);
        }

        [TestMethod]
        public void PlayListTestShouldHaveOneElementWhenSortedLoadingStrategyUsesIndex2InVhkRekFolder() {
            IFolder folder = new FolderNode(Path.Combine(TestConstants.RootFolderPath,"rek"));
            IPlayList playlist = new PlayList(new AllFilesSortedPlayListFactory("{Test;" + folder.FullPath + ";2;false;false;AllFilesSorted}"));
            Assert.AreEqual(2, playlist.Content.Count);
        }

        [TestMethod]
        public void PlayListTestShouldHaveTwoElementsWhenSortedLoadingStrategyUsesIndex3InVhkRekFolder() {
            IFolder folder = new FolderNode(Path.Combine(TestConstants.RootFolderPath, "rek"));
            IPlayList playlist = new PlayList(new AllFilesSortedPlayListFactory("{Test;" + folder.FullPath + ";3;false;false;AllFilesSorted}"));
            Assert.AreEqual(2, playlist.Content.Count);
        }

        [TestMethod]
        public void PlayListTestShouldHave18ElementsWhenFolderLoadingStrategyLoads10sekFolder() {
            IFolder folder = new FolderNode(Path.Combine(TestConstants.RootFolderPath, "10sek"));
            IPlayList playlist = new PlayList(new IteratedFolderPlayListFactory("{Test;" + folder.FullPath + ";true;true;IteratedFolder}"));
            Assert.AreEqual(18, playlist.Content.Count);
        }

        [TestMethod]
        public void PlayListTestShouldHave6ElementsWhenFolderLoadingStrategyLoadsScorRekkFolder() {
            IFolder folder = new FolderNode(Path.Combine(TestConstants.RootFolderPath, "ScorRek"));
            IPlayList playlist = new PlayList(new IteratedFolderPlayListFactory("{Test;" + folder.FullPath + ";true;true;IteratedFolder}"));
            Assert.AreEqual(6, playlist.Content.Count);
        }

        [TestMethod]
        public void PlayListTestShouldGive6FilesWhenPlayCallWithPlayTypePlayListIsUsedWithAllFilesSelectionStrategy() {
            IFolder folder = new FolderNode(Path.Combine(TestConstants.RootFolderPath, "ScorRek"));
            IPlayList playlist = new PlayList(new AllFilesFolderPlayListFactory("{Test;" + folder.FullPath + ";true;true;AllFilesSorted}"));
            Assert.AreEqual(6, playlist.Play(PlayType.PlayList).Count);
        }

        [TestMethod]
        public void PlayListTestShouldGive1FilesWhenPlayCallWithPlayTypePlayListIsUsedWithIteratedSelectionStrategy() {
            IFolder folder = new FolderNode(Path.Combine(TestConstants.RootFolderPath, "ScorRek"));
            IPlayList playlist = new PlayList(new IteratedSortedPlayListFactory("{Test;" + folder.FullPath + ";3;true;true;IteratedFolder}"));
            Assert.AreEqual(1, playlist.Play(PlayType.PlayList).Count);
        }
    }
}