using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Zargess.VHKPlayer.FileManagement.Interfaces;

namespace Zargess.VHKPlayer.FileManagement.Test {
    /// <summary>
    /// Summary description for FolderTest
    /// </summary>
    [TestClass]
    public class FolderTest {
        IFolder _folder;
        
        [TestInitialize()]
        public void BeforeTests() {
            var path = @"D:\Github";
            if (Directory.Exists(path)) {
                Environment.CurrentDirectory = @"D:\GitHub";
                _folder = new FolderNode(@"D:\Dropbox\Programmering\C#\damer 2013-2014");
            } else {
                Environment.CurrentDirectory = @"C:\Users\MFH\Documents\GitHub";
                _folder = new FolderNode(@"c:\users\mfh\vhk");
            }
        }

        [TestMethod]
        public void ProjectFolderNameShouldBeVHKPlayer() {
            IFolder folder = new FolderNode(Environment.CurrentDirectory + @"\VHKPlayer");
            Assert.AreEqual("VHKPlayer", folder.Name);
        }

        [TestMethod]
        public void FolderNameShouldBeGitHub() {
            IFolder folder = new FolderNode(Environment.CurrentDirectory);
            Assert.AreEqual("GitHub", folder.Name);
        }

        [TestMethod]
        public void FolderPathShouldBePathGitHub() {
            IFolder folder = new FolderNode(Environment.CurrentDirectory);
            Assert.AreEqual(Environment.CurrentDirectory, folder.FullPath);
        }

        [TestMethod]
        public void FolderPathShouldBePathVHKPlayer() {
            IFolder folder = new FolderNode(@"VHKPlayer");
            Assert.AreEqual(Environment.CurrentDirectory + @"\VHKPlayer", folder.FullPath);
        }

        [TestMethod]
        public void FolderSourceShouldGitHub() {
            IFolder folder = new FolderNode(Environment.CurrentDirectory + @"\VHKPlayer");
            Assert.AreEqual("GitHub", folder.Source);
        }

        [TestMethod]
        public void FolderSourceShouldVHKPlayer() {
            IFolder folder = new FolderNode(Environment.CurrentDirectory + @"\VHKPlayer\Files for unit test");
            Assert.AreEqual("VHKPlayer", folder.Source);
        }

        [TestMethod]
        public void FolderContentSizeShouldBeFour() {
            IFolder folder = new FolderNode(Environment.CurrentDirectory + @"\VHKPlayer\Files for unit test");
            Assert.AreEqual(3, folder.Content.Count);
        }

        [TestMethod]
        public void FolderContentSizeShouldBeNine() {
            IFolder folder = new FolderNode(Environment.CurrentDirectory + @"\VHKPlayer\Files for unit test\more tests");
            Assert.AreEqual(1, folder.Content.Count);
        }

        [TestMethod]
        public void FolderShouldContainMuse_2_mp3FullPath() {
            IFolder folder = new FolderNode(Environment.CurrentDirectory + @"\VHKPlayer\Files for unit test\more tests");
            Assert.AreEqual(true, folder.ContainsFile(new FileNode(Environment.CurrentDirectory + @"\VHKPlayer\Files for unit test\more tests\muse_2.mp3")));
        }

        [TestMethod]
        public void FolderShouldNotContainTest_txt() {
            IFolder folder = new FolderNode(Environment.CurrentDirectory + @"\VHKPlayer\Files for unit test\more tests");
            Assert.AreEqual(false, folder.ContainsFile(new FileNode(Environment.CurrentDirectory + @"\VHKPlayer\Files for unit test\more tests\Test.txt")));
        }

        [TestMethod]
        public void VHKPlayerFolderShouldContainFolderFiles_for_unit_test() {
            IFolder folder = new FolderNode(Environment.CurrentDirectory + @"\VHKPlayer");
            Assert.AreEqual(true, folder.ContainsFolder(new FolderNode(Environment.CurrentDirectory + @"\VHKPlayer\Files for unit test")));
        }

        [TestMethod]
        public void VHKPlayerFolderShouldContainFolderTemp() {
            IFolder folder = new FolderNode(Environment.CurrentDirectory + @"\VHKPlayer");
            Assert.AreEqual(false, folder.ContainsFolder(new FolderNode(Environment.CurrentDirectory + @"\VHKPlayer\Temp")));
        }

        [TestMethod]
        public void VHKPlayerFolderShouldContainFiles_for_unit_test() {
            IFolder folder = new FolderNode(Environment.CurrentDirectory + @"\VHKPlayer");
            Assert.AreEqual(true, folder.ContainsFolder(new FolderNode(Environment.CurrentDirectory + @"\VHKPlayer\Files for unit test")));
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
        public void FolderStopWatcherCannotBeCalledIfWatcherIsntInitialised() {
            Assert.IsFalse(_folder.StopWatcher());
        }

        [TestMethod]
        public void FolderInitWatcherCanBeCalledAgainAfterStopWatcherIsCalled() {
            _folder.InitWatcher();
            _folder.StopWatcher();
            Assert.IsTrue(_folder.InitWatcher());
        }
    }
}