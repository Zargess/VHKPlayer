using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VHKPlayer.Interfaces;
using VHKPlayer.ViewModels;
using VHKPlayer.Utility;
using VHKPlayer.Models;
using VHKPlayer.Test.Utility;
using VHKPlayer.Strategies.Playing;

namespace VHKPlayer.Test.Models {
    /// <summary>
    /// Summary description for FolderNodeTest
    /// </summary>
    [TestClass]
    public class FolderNodeTest {
        IFolder _folder;

        [TestInitialize]
        public void Setup() {
            var settings = new FolderSettings();
            settings["root"] = TestConstants.RootFolderPath;
            IVideoPlayer videoplayer = new VideoPlayer(settings, new AlternatingPlayStrategy(new PlayFileStrategy(), new PlayPlayerStatStrategy(), new AutoPlayListPlayStrategy(), new DoNothingPlayStrategy()));
            _folder = new FolderNode(TestConstants.RootFolderPath);
        }

        [TestMethod]
        public void FolderNodeTestFolderPathShouldBeRootFolderPath() {
            Assert.AreEqual(TestConstants.RootFolderPath, _folder.FullPath);
        }

        [TestMethod]
        public void FolderNodeTestFolderNameShouldBeVhk() {
            Assert.AreEqual("vhk", _folder.Name);
        }

        [TestMethod]
        public void FolderNodeTestRootFolderPathShouldExist() {
            Assert.IsTrue(_folder.Exists());
        }

        [TestMethod]
        public void FolderNodeTestTempFolderShouldNotExistInRoot() {
            IFolder folder = new FolderNode(TestConstants.RootFolderPath + @"\temp");
            Assert.IsFalse(folder.Exists());
        }

        [TestMethod]
        public void FolderNodeTestRootFolderShouldHave33Files() {
            Assert.AreEqual(4, _folder.Content.Count);
        }

        [TestMethod]
        public void FolderNodeTestRootFolderShouldContainLogo_png() {
            IFile file = new FileNode(TestConstants.RootFolderPath + @"\Logo.png");
            Assert.IsTrue(_folder.ContainsFile(file));
        }

        [TestMethod]
        public void FolderNodeTestRootFolderShouldNotContainTemp_txt() {
            IFile file = new FileNode(TestConstants.RootFolderPath + @"\temp.txt");
            Assert.IsFalse(_folder.ContainsFile(file));
        }

        [TestMethod]
        public void FolderNodeTestDocumentsFolderShouldNotBeValidRootFolder() {
            IFolder folder = new FolderNode(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments));
            Assert.IsFalse(folder.ValidRootFolder());
        }

        [TestMethod]
        public void FolderNodeTestRootFolderShouldBeValidRootFolder() {
            Assert.IsTrue(_folder.ValidRootFolder());
        }
    }
}
