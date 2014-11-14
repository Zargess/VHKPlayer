using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            _file = new FileNode(@"temp:\test");
        }

        [TestMethod]
        public void PlayListNameIsTest() {
            IPlayList playlist = new RepeatablePlayList("Test");
            Assert.AreEqual("Test", playlist.Name);
        }

        [TestMethod]
        public void PlayListNameIs10sek() {
            IPlayList playlist = new RepeatablePlayList("10sek");
            Assert.AreEqual("10sek", playlist.Name);
        }

        [TestMethod]
        public void PlayListGetContentNotNull() {
            IPlayList playlist = new RepeatablePlayList("Temp");
            Assert.IsNotNull(playlist.Content);
        }

        [TestMethod]
        public void ContentShouldHaveSingleItemAfterAdd() {
            IPlayList playlist = new RepeatablePlayList("Temp");
            playlist.Add(_file);
            Assert.AreEqual(1, playlist.Content.Count);
        }

        [TestMethod]
        public void ContentShouldHaveTwoItemsAfterAddingTwice() {
            IPlayList playlist = new RepeatablePlayList("Temp");
            playlist.Add(_file);
            playlist.Add(_file);
            Assert.AreEqual(2, playlist.Content.Count);
        }
    }
}
