using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Zargess.VHKPlayer.FileManagement.Test {
    /// <summary>
    /// Summary description for FileNodeTest
    /// </summary>
    [TestClass]
    public class FileNodeTest {
        public FileNodeTest() {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext {
            get {
                return testContextInstance;
            }
            set {
                testContextInstance = value;
            }
        }

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
        public void FileShouldNotExist() {
            var file = new FileNode(@"q:\helloworld.txt");
            Assert.AreEqual(false, file.Exists());
        }

        [TestMethod]
        public void FileShouldExist() {
            var file = new FileNode(Environment.CurrentDirectory + @"\Test.txt");
            Assert.AreEqual(true, file.Exists());
        }
    }
}
