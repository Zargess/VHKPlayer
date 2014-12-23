using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.Model;
using Zargess.VHKPlayer.Enums;

namespace Zargess.VHKPlayer.Test {
    /// <summary>
    /// Summary description for FileTest
    /// </summary>
    [TestClass]
    public class FileTest {
        [TestInitialize()]
        public void BeforeTests() {
            Constants.GetRootFolder();
        }

        [TestMethod]
        public void Test_txtNameShouldBeTest_txt() {
            IFile file = new FileNode("Test.txt");
            Assert.AreEqual("Test.txt", file.Name);
        }

        [TestMethod]
        public void PathToTest_txtNameShouldBeTest_txt() {
            IFile file = new FileNode(Environment.CurrentDirectory + @"\Test.txt");
            Assert.AreEqual("Test.txt", file.Name);
        }

        [TestMethod]
        public void Test_txtPathShouldBePath_Test_txt() {
            IFile file = new FileNode("Test.txt");
            Assert.AreEqual(Environment.CurrentDirectory + @"\Test.txt", file.FullPath);
        }

        [TestMethod]
        public void Logo_pngPathShouldBePath_Logo_png() {
            IFile file = new FileNode("Logo.png");
            Assert.AreEqual(Environment.CurrentDirectory + @"\Logo.png", file.FullPath);
        }

        [TestMethod]
        public void PathToLogo_pngPathShouldBePath_Logo_png() {
            IFile file = new FileNode(Environment.CurrentDirectory + @"\Logo.png");
            Assert.AreEqual(Environment.CurrentDirectory + @"\Logo.png", file.FullPath);
        }

        [TestMethod]
        public void SourceOfLogo_pngPathShouldBeFiles_for_unit_test() {
            IFile file = new FileNode(Environment.CurrentDirectory + @"\Logo.png");
            Assert.AreEqual("Files for unit test", file.Source);
        }

        [TestMethod]
        public void Logo_pngShouldExist() {
            IFile file = new FileNode(Environment.CurrentDirectory + @"\Logo.png");
            Assert.IsTrue(file.Exists());
        }

        [TestMethod]
        public void Temp_pngShouldNotExist() {
            IFile file = new FileNode(Environment.CurrentDirectory + @"\Temp.png");
            Assert.IsFalse(file.Exists());
        }

        [TestMethod]
        public void TypeOfLogo_pngShouldBePicture() {
            IFile file = new FileNode(Environment.CurrentDirectory + @"\Logo.png");
            Assert.AreEqual(FileType.Picture, file.Type);
        }

        [TestMethod]
        public void TypeOfTest_txtShouldBeUnsupported() {
            IFile file = new FileNode(Environment.CurrentDirectory + @"\Test.txt");
            Assert.AreEqual(FileType.Unsupported, file.Type);
        }
    }
}
