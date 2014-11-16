using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zargess.VHKPlayer.FileManagement;
using Zargess.VHKPlayer.FileManagement.Strategies.Loading;
using System.IO;

namespace Zargess.VHKPlayer.FileManagement.Test {
    /// <summary>
    /// Summary description for PlayListTest
    /// </summary>
    [TestClass]
    public class PlayListTest {
        private IFile _file;
        private IPlayList _playlist;
        private IFolder _folder;
        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestInitialize]
        public void Setup() {
            var path = @"D:\Github";
            if (Directory.Exists(path)) {
                _folder = new FolderNode(@"D:\Dropbox\Programmering\C#\damer 2013-2014");
            } else {
                _folder = new FolderNode(@"c:\users\mfh\vhk");
            }
            _file = new FileNode(@"c:\test.txt");
            _playlist = new PlayList("Test", _folder, new AllFileSelectionStrategy(), new NoLoadingStrategy());
        }

        [TestMethod]
        public void PlayListNameIsTest() {
            IPlayList playlist = new PlayList("Test", _folder, new AllFileSelectionStrategy(), new NoLoadingStrategy());
            Assert.AreEqual("Test", playlist.Name);
        }

        [TestMethod]
        public void PlayListNameIs10sek() {
            IPlayList playlist = new PlayList("10sek", _folder, new AllFileSelectionStrategy(), new NoLoadingStrategy());
            Assert.AreEqual("10sek", playlist.Name);
        }

        [TestMethod]
        public void PlayListGetContentNotNull() {
            Assert.IsNotNull(_playlist.GetContent());
        }

        [TestMethod]
        public void ContentShouldHaveSingleItemAfterAdd() {
            _playlist.Add(_file);
            Assert.AreEqual(1, _playlist.Size);
        }

        [TestMethod]
        public void ContentShouldHaveTwoItemsAfterAddingTwice() {
            _playlist.Add(_file);
            _playlist.Add(_file);
            Assert.AreEqual(2, _playlist.Size);
        }

        [TestMethod]
        public void PlayCallOnPlayListWithOneFileShouldReturnQueueWithOneFile() {
            _playlist.Add(_file);
            var queue = _playlist.Play();
            Assert.AreEqual(1, queue.Count);
        }

        [TestMethod]
        public void PlayCallOnPlayListWithTwoFilesShouldReturnQueueWithTwoFiles() {
            _playlist.Add(_file);
            _playlist.Add(_file);
            var queue = _playlist.Play();
            Assert.AreEqual(2, queue.Count);
        }

        [TestMethod]
        public void PlayCallOnRepeatablePlayListWithTwoFileShouldReturnQueueWithOneFile() {
            IPlayList playlist = new PlayList("Test", _folder, new IteratedFileSelectionStrategy(), new NoLoadingStrategy());
            playlist.Add(_file);
            playlist.Add(_file);
            var queue = playlist.Play();
            Assert.AreEqual(1, queue.Count);
        }

        [TestMethod]
        public void SecondPlayCallOnRepeatablePlayListWithTwoWillReturnSecondFile() {
            IPlayList playlist = new PlayList("Test", _folder, new IteratedFileSelectionStrategy(), new NoLoadingStrategy());
            IFile file2 = new FileNode("c:\test.txt");
            playlist.Add(_file);
            playlist.Add(file2);
            playlist.Play();
            var queue = playlist.Play();
            Assert.AreEqual(file2.FullPath, queue.Dequeue().FullPath);
        }

        [TestMethod]
        public void PlayListShouldNotHaveContentWhenHasNoLoadingStrategy() {
            Assert.AreEqual(0, _playlist.Size);
        }

        [TestMethod]
        public void PlayListShouldHaveOneElementWhenSortedLoadingStrategyUsesIndex2InVhkRekFolder() {
            IFolder folder = new FolderNode(_folder.FullPath + @"\rek");
            IPlayList playlist = new PlayList("Test", folder, new AllFileSelectionStrategy(), new SortedLoadingStrategy(2, folder));
            Assert.AreEqual(1, playlist.Size);
        }

        [TestMethod]
        public void PlayListShouldHaveTwoElementsWhenSortedLoadingStrategyUsesIndex3InVhkRekFolder() {
            IFolder folder = new FolderNode(_folder.FullPath + @"\rek");
            IPlayList playlist = new PlayList("Test", folder, new AllFileSelectionStrategy(), new SortedLoadingStrategy(3, folder));
            Assert.AreEqual(2, playlist.Size);
        }

        [TestMethod]
        public void PlayListShouldHave18ElementsWhenFolderLoadingStrategyLoads10sekFolder() {
            IFolder folder = new FolderNode(_folder.FullPath + @"\10sek");
            IPlayList playlist = new PlayList("Test", folder, new IteratedFileSelectionStrategy(), new FolderLoadingStrategy(folder));
            Assert.AreEqual(18, playlist.Size);
        }

        [TestMethod]
        public void PlayListShouldHave6ElementsWhenFolderLoadingStrategyLoadsScorRekkFolder() {
            IFolder folder = new FolderNode(_folder.FullPath + @"\ScorRek");
            IPlayList playlist = new PlayList("Test", folder, new IteratedFileSelectionStrategy(), new FolderLoadingStrategy(folder));
            Assert.AreEqual(6, playlist.Size);
        }
    }
}