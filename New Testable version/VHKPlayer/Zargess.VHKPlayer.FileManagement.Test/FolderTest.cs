using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Zargess.VHKPlayer.FileManagement.Test {
    /// <summary>
    /// Summary description for FolderTest
    /// </summary>
    [TestClass]
    public class FolderTest {
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
            Environment.CurrentDirectory = @"C:\Users\MFH\Documents\GitHub\VHKPlayer\";
        }

        [TestMethod]
        public void ProjectFolderNameShouldBeVHKPlayer() {
            IFolder folder = new FolderNode(@"C:\Users\MFH\Documents\GitHub\VHKPlayer\");
            Assert.AreEqual("VHKPlayer", folder.Name);
        }

        [TestMethod]
        public void FolderNameShouldBeGitHub() {
            IFolder folder = new FolderNode(@"C:\Users\MFH\Documents\GitHub\");
            Assert.AreEqual("GitHub", folder.Name);
        }

        [TestMethod]
        public void FolderPathShouldBePathGitHub() {
            IFolder folder = new FolderNode(@"C:\Users\MFH\Documents\GitHub\");
            Assert.AreEqual(@"C:\Users\MFH\Documents\GitHub\", folder.FullPath);
        }

        [TestMethod]
        public void FolderPathShouldBePathVHKPlayer() {
            IFolder folder = new FolderNode(@"C:\Users\MFH\Documents\GitHub\VHKPlayer\");
            Assert.AreEqual(@"C:\Users\MFH\Documents\GitHub\VHKPlayer\", folder.FullPath);
        }
    }
}
