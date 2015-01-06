using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.Model;
using Zargess.VHKPlayer.Utility;
using System.IO;

namespace Zargess.VHKPlayer.Test {
    /// <summary>
    /// Summary description for FolderTest
    /// </summary>
    [TestClass]
    public class FolderTest {
        IFolder _folder;

        [TestInitialize()]
        public void BeforeTests() {
            _folder = Constants.GetRootFolder();
        }

        [TestMethod]
        public void FolderNameShouldBeMoretests() {
            IFolder folder = new FolderNode(Environment.CurrentDirectory + @"\more tests");
            Assert.AreEqual("more tests", folder.Name);
        }

        [TestMethod]
        public void FolderNameShouldBeFilesForUnitTest() {
            IFolder folder = new FolderNode(Environment.CurrentDirectory);
            Assert.AreEqual("Files for unit test", folder.Name);
        }

        [TestMethod]
        public void FolderPathShouldBePathGitHub() {
            IFolder folder = new FolderNode(Environment.CurrentDirectory);
            Assert.AreEqual(Environment.CurrentDirectory, folder.FullPath);
        }

        [TestMethod]
        public void FolderPathShouldBePathVHKPlayer() {
            var path = Environment.CurrentDirectory.Replace(@"\Files for unit test", "");
            IFolder folder = new FolderNode(path);
            Assert.AreEqual(path, folder.FullPath);
        }

        [TestMethod]
        public void FolderSourceShouldVHKPlayer() {
            IFolder folder = new FolderNode(Environment.CurrentDirectory);
            Assert.AreEqual("VHKPlayer", folder.Source);
        }

        [TestMethod]
        public void FolderContentSizeShouldBeFour() {
            IFolder folder = new FolderNode(Environment.CurrentDirectory);
            Assert.AreEqual(3, folder.Content.Count);
        }

        [TestMethod]
        public void FolderContentSizeShouldBeNine() {
            IFolder folder = new FolderNode(Environment.CurrentDirectory + @"\more tests");
            Assert.AreEqual(1, folder.Content.Count);
        }

        [TestMethod]
        public void FolderShouldContainMuse_2_mp3FullPath() {
            IFolder folder = new FolderNode(Environment.CurrentDirectory + @"\more tests");
            Assert.AreEqual(true, folder.ContainsFile(new FileNode(Environment.CurrentDirectory + @"\more tests\muse_2.mp3")));
        }

        [TestMethod]
        public void FolderShouldNotContainTest_txt() {
            IFolder folder = new FolderNode(Environment.CurrentDirectory + @"\VHKPlayer\Files for unit test\more tests");
            Assert.AreEqual(false, folder.ContainsFile(new FileNode(Environment.CurrentDirectory + @"\VHKPlayer\Files for unit test\more tests\Test.txt")));
        }

        [TestMethod]
        public void VHKPlayerFolderShouldContainFolderFiles_for_unit_test() {
            var path = Environment.CurrentDirectory.Replace(@"\Files for unit test", "");
            IFolder folder = new FolderNode(path);
            Assert.AreEqual(true, folder.ContainsFolder(new FolderNode(path + @"\Files for unit test")));
        }

        [TestMethod]
        public void VHKPlayerFolderShouldContainFolderTemp() {
            var path = Environment.CurrentDirectory.Replace(@"\Files for unit test", "");
            IFolder folder = new FolderNode(path);
            Assert.AreEqual(false, folder.ContainsFolder(new FolderNode(Environment.CurrentDirectory + @"\Temp")));
        }

        [TestMethod]
        public void Files_For_unit_testFolderShouldContainMore_Tests() {
            IFolder folder = new FolderNode(Environment.CurrentDirectory);
            Assert.AreEqual(true, folder.ContainsFolder(new FolderNode(Environment.CurrentDirectory + @"\more tests")));
        }

        [TestMethod]
        public void FolderInitWatcherCallInitialisesWatcher() {
            _folder.InitWatcher();
            Assert.IsNotNull(_folder.Watcher);
        }

        [TestMethod]
        public void FolderInitWatcherCallEnablesRaisingEvents() {
            _folder.InitWatcher();
            Assert.IsTrue(_folder.Watcher.EnableRaisingEvents);
        }

        [TestMethod]
        public void FolderCannotCallInitWatcherIfItsInitialised() {
            _folder.InitWatcher();
            Assert.IsFalse(_folder.InitWatcher());
        }

        [TestMethod]
        public void FolderCannotInitialiseWatcherIfFolderDoesntExist() {
            var folder = new FolderNode("c:\test");
            Assert.IsFalse(folder.InitWatcher());
        }

        [TestMethod]
        public void FolderWatcherIsNotInitialisedAfterStopWatcherIsCalled() {
            _folder.InitWatcher();
            _folder.StopWatcher();
            Assert.IsNull(_folder.Watcher);
        }

        [TestMethod]
        public void FolderStopWatcherCanBeCalledIfWatcherIsntInitialised() {
            Assert.IsTrue(_folder.StopWatcher());
        }

        [TestMethod]
        public void FolderInitWatcherCanBeCalledAgainAfterStopWatcherIsCalled() {
            _folder.InitWatcher();
            _folder.StopWatcher();
            Assert.IsTrue(_folder.InitWatcher());
        }

        [TestMethod]
        public void FolderShouldBeValidRootFolder() {
            Assert.IsTrue(_folder.ValidRootFolder());
        }

        [TestMethod]
        public void SpillerFolderShouldNotBeValidRootFolder() {
            var folder = new FolderNode(PathHandler.CombinePaths(_folder.FullPath, "Spiller"));
            Assert.IsFalse(folder.ValidRootFolder());
        }
    }
}
