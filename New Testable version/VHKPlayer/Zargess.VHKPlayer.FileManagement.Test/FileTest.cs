using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            Environment.CurrentDirectory = @"C:\Users\MFH\Documents\GitHub\VHKPlayer\Files for unit test";
        }

        [TestMethod]
        public void Test_txtNameShouldBeTest_txt() {
            IFile file = new FileNode("Test.txt");
            Assert.AreEqual("Test.txt", file.Name);
        }

        [TestMethod]
        public void PathToTest_txtNameShouldBeTest_txt() {
            IFile file = new FileNode(@"C:\Users\MFH\Documents\GitHub\VHKPlayer\Files for unit test\Test.txt");
            Assert.AreEqual("Test.txt", file.Name);
        }

        [TestMethod]
        public void Test_txtPathShouldBePath_Test_txt() {
            IFile file = new FileNode("Test.txt");
            Assert.AreEqual(@"C:\Users\MFH\Documents\GitHub\VHKPlayer\Files for unit test\Test.txt", file.FullPath);
        }

        [TestMethod]
        public void Logo_pngPathShouldBePath_Logo_png() {
            IFile file = new FileNode("Logo.png");
            Assert.AreEqual(@"C:\Users\MFH\Documents\GitHub\VHKPlayer\Files for unit test\Logo.png", file.FullPath);
        }

        [TestMethod]
        public void PathToLogo_pngPathShouldBePath_Logo_png() {
            IFile file = new FileNode(@"C:\Users\MFH\Documents\GitHub\VHKPlayer\Files for unit test\Logo.png");
            Assert.AreEqual(@"C:\Users\MFH\Documents\GitHub\VHKPlayer\Files for unit test\Logo.png", file.FullPath);
        }

        [TestMethod]
        public void SourceOfLogo_pngPathShouldBeFiles_for_unit_test() {
            IFile file = new FileNode(@"C:\Users\MFH\Documents\GitHub\VHKPlayer\Files for unit test\Logo.png");
            Assert.AreEqual("Files for unit test", file.Source);
        }

        [TestMethod]
        public void SourceOfTestList_txtPathShouldBeVHKPlayer() {
            IFile file = new FileNode(@"C:\Users\MFH\Documents\GitHub\VHKPlayer\TestList.txt");
            Assert.AreEqual("VHKPlayer", file.Source);
        }

        [TestMethod]
        public void Logo_pngShouldExist() {
            IFile file = new FileNode(@"C:\Users\MFH\Documents\GitHub\VHKPlayer\Files for unit test\Logo.png");
            Assert.IsTrue(file.Exists());
        }

        [TestMethod]
        public void Temp_pngShouldNotExist() {
            IFile file = new FileNode(@"C:\Users\MFH\Documents\GitHub\VHKPlayer\Files for unit test\Temp.png");
            Assert.IsFalse(file.Exists());
        }

        [TestMethod]
        public void TypeOfLogo_pngShouldBePicture() {
            IFile file = new FileNode(@"C:\Users\MFH\Documents\GitHub\VHKPlayer\Files for unit test\Logo.png");
            Assert.AreEqual(FileType.Picture, file.Type);
        }

        [TestMethod]
        public void TypeOfTest_txtShouldBeUnsupported() {
            IFile file = new FileNode(@"C:\Users\MFH\Documents\GitHub\VHKPlayer\Files for unit test\Test.txt");
            Assert.AreEqual(FileType.Unsupported, file.Type);
        }
    }
}
