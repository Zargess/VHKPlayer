using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VHKPlayer.Interfaces;
using VHKPlayer.Test.Utility;
using VHKPlayer.Models;
using VHKPlayer.Enums;

namespace VHKPlayer.Test.Models {
    /// <summary>
    /// Summary description for FileNodeTest
    /// </summary>
    [TestClass]
    public class FileNodeTest {
        private IFile _file;
        [TestInitialize]
        public void Setup() {
            Environment.CurrentDirectory = Constants.RootFolderPath;
            _file = new FileNode(Constants.RootFolderPath + @"\Dong.mp3");
        }

        [TestMethod]
        public void FileNodeTestDong_mp3FullPathShouldBeRootFolderPathDong_mp3() {
            Assert.AreEqual(Constants.RootFolderPath + @"\Dong.mp3", _file.FullPath);
        }

        [TestMethod]
        public void FileNodeTestDong_mp3NameShouldBeDong_mp3() {
            Assert.AreEqual("Dong.mp3", _file.Name);
        }

        [TestMethod]
        public void FileNodeTestDong_mp3NameWithoutExtensionShouldBeDong() {
            Assert.AreEqual("Dong", _file.NameWithoutExtension);
        }

        [TestMethod]
        public void FileNodeTestDong_mp3TypeShouldBeFileType_Music() {
            Assert.AreEqual(FileType.Music, _file.Type);
        }

        [TestMethod]
        public void FileNodeTestLogo_pngTypeShouldBePicture() {
            IFile file = new FileNode(Constants.RootFolderPath + @"\Logo.png");
            Assert.AreEqual(FileType.Picture, file.Type);
        }
    }
}
