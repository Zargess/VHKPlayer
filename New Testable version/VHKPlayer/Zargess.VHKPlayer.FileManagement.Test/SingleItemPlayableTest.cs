using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zargess.VHKPlayer.FileManagement.Strategies.Loading.IPlayable;
using System.IO;

namespace Zargess.VHKPlayer.FileManagement.Test {
    /// <summary>
    /// Summary description for SingleItemPlayableTest
    /// </summary>
    [TestClass]
    public class SingleItemPlayableTest {
        private IPlayable _single;

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

        [TestInitialize]
        public void Setup() {
            var path = @"D:\Github";
            if (Directory.Exists(path)) {
                _single = new SingleItemPlayable(new FileLoadingStrategy(@"D:\Dropbox\Programmering\C#\damer 2013-2014\musik\Andet\muse_2.mp3"));
            } else {
                _single = new SingleItemPlayable(new FileLoadingStrategy(@"C:\Users\MFH\vhk\musik\Andet\muse_2.mp3"));
            }
            
        }

        [TestMethod]
        public void SingleItemPlayableContentSizeShouldBeOneAfterConstruction() {
            Assert.AreEqual(1, _single.Size);
        }

        [TestMethod]
        public void SingleItemPlayableCannotAddTest_txtToItAndShouldHaveNoFilesInContent() {
            IPlayable single = new SingleItemPlayable(new FileLoadingStrategy(@"C:\Users\MFH\vhk\musik\Andet\Test.txt"));
            Assert.AreEqual(0, single.Size);
        }

        [TestMethod]
        public void SingleItemPlayableNameShouldBemuse_2_mp3() {
            Assert.AreEqual("muse_2.mp3", _single.Name);
        }

        [TestMethod]
        public void SingleItemPlayableNameShouldBeNullWhenFileDoesntExist() {
            IPlayable single = new SingleItemPlayable(new FileLoadingStrategy(@"C:\Users\MFH\vhk\musik\Andet\Test.txt"));
            Assert.IsNull(single.Name);
        }

        [TestMethod]
        public void SingleItemPlayableCountOnQueueFromPlayCallShouldBe1() {
            Assert.AreEqual(1, _single.Play(PlayType.PlayList).Count);
        }

        [TestMethod]
        public void EmptySingleItemPlayablePlayCallShouldResultInEmptyQueue() {
            IPlayable single = new SingleItemPlayable(new FileLoadingStrategy(@"C:\Users\MFH\vhk\musik\Andet\Test.txt"));
            Assert.AreEqual(0, single.Play(PlayType.PlayList).Count);
        }
    }
}
