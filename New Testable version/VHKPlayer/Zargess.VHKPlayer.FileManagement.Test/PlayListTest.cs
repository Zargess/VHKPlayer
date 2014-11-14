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
        }

        [TestMethod]
        public void PlayListNameIsTest() {
            IPlayList playlist = new PlayList("Test", new AllFileSelectionStrategy());
            Assert.AreEqual("Test", playlist.Name);
        }

        [TestMethod]
        public void PlayListNameIs10sek() {
            IPlayList playlist = new PlayList("10sek", new AllFileSelectionStrategy());
            Assert.AreEqual("10sek", playlist.Name);
        }

        [TestMethod]
        public void PlayListGetContentNotNull() {
            IPlayList playlist = new PlayList("Temp", new AllFileSelectionStrategy());
            Assert.IsNotNull(playlist.Content);
        }

        [TestMethod]
        public void ContentShouldHaveSingleItemAfterAdd() {
            IPlayList playlist = new PlayList("Temp", new AllFileSelectionStrategy());
            playlist.Add(_file);
            Assert.AreEqual(1, playlist.Content.Count);
        }

        [TestMethod]
        public void ContentShouldHaveTwoItemsAfterAddingTwice() {
            IPlayList playlist = new PlayList("Temp", new AllFileSelectionStrategy());
            playlist.Add(_file);
            playlist.Add(_file);
            Assert.AreEqual(2, playlist.Content.Count);
        }

        [TestMethod]
        public void PlayCallOnPlayListWithOneFileShouldReturnQueueWithOneFile() {
            IPlayList playlist = new PlayList("Test", new AllFileSelectionStrategy());
            playlist.Add(_file);
            var queue = playlist.Play();
            Assert.AreEqual(1, queue.Count);
        }

        [TestMethod]
        public void PlayCallOnPlayListWithTwoFilesShouldReturnQueueWithTwoFiles() {
            IPlayList playlist = new PlayList("Test", new AllFileSelectionStrategy());
            playlist.Add(_file);
            playlist.Add(_file);
            var queue = playlist.Play();
            Assert.AreEqual(2, queue.Count);
        }

        [TestMethod]
        public void PlayCallOnRepeatablePlayListWithTwoFileShouldReturnQueueWithOneFile() {
            IPlayList playlist = new PlayList("Test", new IteratedFileSelectionStrategy());
            playlist.Add(_file);
            playlist.Add(_file);
            var queue = playlist.Play();
            Assert.AreEqual(1, queue.Count);
        }

        [TestMethod]
        public void SecondPlayCallOnRepeatablePlayListWithTwoWillReturnSecondFile() {
            IPlayList playlist = new PlayList("Test", new IteratedFileSelectionStrategy());
            IFile file2 = new FileNode("c:\test.txt");
            playlist.Add(_file);
            playlist.Add(file2);
            playlist.Play();
            var queue = playlist.Play();
            Assert.AreEqual(file2.FullPath, queue.Dequeue().FullPath);
        }


    }
}
