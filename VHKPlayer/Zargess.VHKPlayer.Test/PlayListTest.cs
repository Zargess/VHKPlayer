using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zargess.VHKPlayer.Interfaces;
using System.IO;
using Zargess.VHKPlayer.Model;
using Zargess.VHKPlayer.Factories.IPlayLists;
using Zargess.VHKPlayer.Enums;

namespace Zargess.VHKPlayer.Test {
    /// <summary>
    /// Summary description for PlayListTest
    /// </summary>
    [TestClass]
    public class PlayListTest {
        private IFile _file;
        private IPlayList _playlist;
        private IFolder _folder;

        [TestInitialize]
        public void Setup() {
            var path = @"D:\Github";
            if (Directory.Exists(path)) {
                _folder = new FolderNode(@"D:\Dropbox\Programmering\C#\damer 2013-2014");
            } else {
                _folder = new FolderNode(@"c:\users\mfh\vhk");
            }
            _file = new FileNode(@"c:\test.txt");
            _playlist = new PlayList(new AllFilesNoLoadingFactory("{Test;" + _folder.FullPath + ";false}"));
        }

        [TestMethod]
        public void PlayListNameIsTest() {
            Assert.AreEqual("Test", _playlist.Name);
        }

        [TestMethod]
        public void PlayListNameIs10sek() {
            IPlayList playlist = new PlayList(new AllFilesNoLoadingFactory("{10sek;" + _folder.FullPath + ";false}"));
            Assert.AreEqual("10sek", playlist.Name);
        }

        [TestMethod]
        public void PlayListGetContentNotNull() {
            Assert.IsNotNull(_playlist.Content);
        }

        [TestMethod]
        public void ContentShouldHaveSingleItemAfterAdd() {
            _playlist.Add(_file);
            Assert.AreEqual(1, _playlist.Content.Count);
        }

        [TestMethod]
        public void ContentShouldHaveTwoItemsAfterAddingTwice() {
            _playlist.Add(_file);
            _playlist.Add(_file);
            Assert.AreEqual(2, _playlist.Content.Count);
        }

        [TestMethod]
        public void PlayCallOnPlayListWithOneFileShouldReturnQueueWithOneFile() {
            _playlist.Add(_file);
            var queue = _playlist.Play(PlayType.PlayList);
            Assert.AreEqual(1, queue.Count);
        }

        [TestMethod]
        public void PlayCallOnPlayListWithTwoFilesShouldReturnQueueWithTwoFiles() {
            _playlist.Add(_file);
            _playlist.Add(_file);
            var queue = _playlist.Play(PlayType.PlayList);
            Assert.AreEqual(2, queue.Count);
        }

        [TestMethod]
        public void PlayCallOnRepeatablePlayListWithTwoFileShouldReturnQueueWithOneFile() {
            IPlayList playlist = new PlayList(new IteratedNoLoadingFactory("{Test;" + _folder.FullPath + ";true}"));
            playlist.Add(_file);
            playlist.Add(_file);
            var queue = playlist.Play(PlayType.PlayList);
            Assert.AreEqual(1, queue.Count);
        }

        [TestMethod]
        public void SecondPlayCallOnRepeatablePlayListWithTwoWillReturnSecondFile() {
            IPlayList playlist = new PlayList(new IteratedNoLoadingFactory("{Test;" + _folder.FullPath + ";true}"));
            IFile file2 = new FileNode("c:\test.txt");
            playlist.Add(_file);
            playlist.Add(file2);
            playlist.Play(PlayType.PlayList);
            var queue = playlist.Play(PlayType.PlayList);
            Assert.AreEqual(file2.FullPath, queue.Dequeue().FullPath);
        }

        [TestMethod]
        public void PlayListShouldNotHaveContentWhenHasNoLoadingStrategy() {
            Assert.AreEqual(0, _playlist.Content.Count);
        }

        [TestMethod]
        public void PlayListShouldHaveOneElementWhenSortedLoadingStrategyUsesIndex2InVhkRekFolder() {
            IFolder folder = new FolderNode(_folder.FullPath + @"\rek");
            IPlayList playlist = new PlayList(new AllFilesSortedPlayListFactory("{Test;" + folder.FullPath + ";2;false}"));
            Assert.AreEqual(1, playlist.Content.Count);
        }

        [TestMethod]
        public void PlayListShouldHaveTwoElementsWhenSortedLoadingStrategyUsesIndex3InVhkRekFolder() {
            IFolder folder = new FolderNode(_folder.FullPath + @"\rek");
            IPlayList playlist = new PlayList(new AllFilesSortedPlayListFactory("{Test;" + folder.FullPath + ";3;false}"));
            Assert.AreEqual(2, playlist.Content.Count);
        }

        [TestMethod]
        public void PlayListShouldHave18ElementsWhenFolderLoadingStrategyLoads10sekFolder() {
            IFolder folder = new FolderNode(_folder.FullPath + @"\10sek");
            IPlayList playlist = new PlayList(new IteratedFolderPlayListFactory("{Test;" + folder.FullPath + ";true}"));
            Assert.AreEqual(18, playlist.Content.Count);
        }

        [TestMethod]
        public void PlayListShouldHave6ElementsWhenFolderLoadingStrategyLoadsScorRekkFolder() {
            IFolder folder = new FolderNode(_folder.FullPath + @"\ScorRek");
            IPlayList playlist = new PlayList(new IteratedFolderPlayListFactory("{Test;" + folder.FullPath + ";true}"));
            Assert.AreEqual(6, playlist.Content.Count);
        }
    }
}
