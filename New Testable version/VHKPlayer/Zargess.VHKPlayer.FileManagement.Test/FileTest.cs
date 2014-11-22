using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Zargess.VHKPlayer.FileManagement.Interfaces;

namespace Zargess.VHKPlayer.FileManagement.Test {
    /// <summary>
    /// Summary description for FileTest
    /// </summary>
    [TestClass]
    public class FileTest {
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

        [TestInitialize()]
        public void BeforeTests() {
            var path = @"D:\Github";
            if (Directory.Exists(path)) {
                Environment.CurrentDirectory = @"D:\GitHub\VHKPlayer\Files for unit test";
            } else {
                Environment.CurrentDirectory = @"C:\Users\MFH\Documents\GitHub\VHKPlayer\Files for unit test";
            }
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
