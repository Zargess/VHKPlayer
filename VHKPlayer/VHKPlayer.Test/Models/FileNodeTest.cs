using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VHKPlayer.Interfaces;
using VHKPlayer.Test.Utility;
using VHKPlayer.Models;
using VHKPlayer.Enums;
using VHKPlayer.ViewModels;
using VHKPlayer.Utility;
using VHKPlayer.Strategies.Playing;

namespace VHKPlayer.Test.Models {
    /// <summary>
    /// Summary description for FileNodeTest
    /// </summary>
    [TestClass]
    public class FileNodeTest {
        private IFile _file;

        [TestInitialize]
        public void Setup() {
            var settings = new FolderSettings();
            settings["root"] = TestConstants.RootFolderPath;
            IVideoPlayer videoplayer = new VideoPlayer(settings, new AlternatingPlayStrategy(new PlayFileStrategy(), new PlayPlayerStatStrategy()));
            _file = new FileNode(TestConstants.RootFolderPath + @"\dong.mp3");
        }

        [TestMethod]
        public void FileNodeTestDong_mp3FullPathShouldBeRootFolderPathDong_mp3() {
            Assert.AreEqual(TestConstants.RootFolderPath + @"\dong.mp3", _file.FullPath);
        }

        [TestMethod]
        public void FileNodeTestDong_mp3NameShouldBeDong_mp3() {
            Assert.AreEqual("dong.mp3", _file.Name);
        }

        [TestMethod]
        public void FileNodeTestDong_mp3NameWithoutExtensionShouldBeDong() {
            Assert.AreEqual("dong", _file.NameWithoutExtension);
        }

        [TestMethod]
        public void FileNodeTestDong_mp3TypeShouldBeFileType_Music() {
            Assert.AreEqual(FileType.Music, _file.Type);
        }

        [TestMethod]
        public void FileNodeTestLogo_pngTypeShouldBePicture() {
            IFile file = new FileNode(TestConstants.RootFolderPath + @"\Logo.png");
            Assert.AreEqual(FileType.Picture, file.Type);
        }

        [TestMethod]
        public void FileNodeTestTemp_aviTypeShouldBeVideo() {
            IFile file = new FileNode(TestConstants.RootFolderPath + @"\temp.avi");
            Assert.AreEqual(FileType.Video, file.Type);
        }

        [TestMethod]
        public void FileNodeTestTemp_xmlTypeShouldBeInfo() {
            IFile file = new FileNode(TestConstants.RootFolderPath + @"\temp.xml");
            Assert.AreEqual(FileType.Info, file.Type);
        }

        [TestMethod]
        public void FileNodeTestTemp_txtTypeShouldBeUnsupported() {
            IFile file = new FileNode(TestConstants.RootFolderPath + @"\temp.txt");
            Assert.AreEqual(FileType.Unsupported, file.Type);
        }

        [TestMethod]
        public void FileNodeTestDong_mp3ShouldExist() {
            Assert.IsTrue(_file.Exists());
        }

        [TestMethod]
        public void FileNodeTestTemp_aviShouldNotExist() {
            IFile file = new FileNode(TestConstants.RootFolderPath + @"\temp.avi");
            Assert.IsFalse(file.Exists());
        }
    }
}
