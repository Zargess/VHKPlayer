using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zargess.VHKPlayer.FileManagement;

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
            _file = new FileNode(@"c:\test.txt");
            _folder = new FolderNode(@"c:\users\mfh\vhk");
            _playlist = new PlayList("Test", _folder, new AllFileSelectionStrategy());
        }

        [TestMethod]
        public void PlayListNameIsTest() {
            IPlayList playlist = new PlayList("Test", _folder, new AllFileSelectionStrategy());
            Assert.AreEqual("Test", playlist.Name);
        }

        [TestMethod]
        public void PlayListNameIs10sek() {
            IPlayList playlist = new PlayList("10sek", _folder, new AllFileSelectionStrategy());
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
            IPlayList playlist = new PlayList("Test", _folder, new IteratedFileSelectionStrategy());
            playlist.Add(_file);
            playlist.Add(_file);
            var queue = playlist.Play();
            Assert.AreEqual(1, queue.Count);
        }

        [TestMethod]
        public void SecondPlayCallOnRepeatablePlayListWithTwoWillReturnSecondFile() {
            IPlayList playlist = new PlayList("Test", _folder, new IteratedFileSelectionStrategy());
            IFile file2 = new FileNode("c:\test.txt");
            playlist.Add(_file);
            playlist.Add(file2);
            playlist.Play();
            var queue = playlist.Play();
            Assert.AreEqual(file2.FullPath, queue.Dequeue().FullPath);
        }

        [TestMethod]
        public void PlayListInitWatcherCallInitialisesWatcher() {
            _playlist.InitWatcher();
            Assert.IsNotNull(_playlist.Watcher);
        }

        [TestMethod]
        public void PlayListInitWatcherCallEnablesRaisingEvents() {
            _playlist.InitWatcher();
            Assert.IsTrue(_playlist.Watcher.EnableRaisingEvents);
        }

        [TestMethod]
        public void PlayListCannotCallInitWatcherIfItsInitialised() {
            _playlist.InitWatcher();
            Assert.IsFalse(_playlist.InitWatcher());
        }

        [TestMethod]
        public void PlayListCannotInitialiseWatcherIfFolderDoesntExist() {
            var playlist = new PlayList("Test", new FolderNode("c:\test"), new AllFileSelectionStrategy());
            Assert.IsFalse(playlist.InitWatcher());
        }

        [TestMethod]
        public void PlayListWatcherIsNotInitialisedAfterStopWatcherIsCalled() {
            _playlist.InitWatcher();
            _playlist.StopWatcher();
            Assert.IsNull(_playlist.Watcher);
        }

        [TestMethod]
        public void PlayListStopWatcherCannotBeCalledIfWatcherIsntInitialised() {
            Assert.IsFalse(_playlist.StopWatcher());
        }

        [TestMethod]
        public void PlayListInitWatcherCanBeCalledAgainAfterStopWatcherIsCalled() {
            _playlist.InitWatcher();
            _playlist.StopWatcher();
            Assert.IsTrue(_playlist.InitWatcher());
        }
    }
}
