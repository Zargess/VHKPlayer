using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.Model;
using Zargess.VHKPlayer.Strategies.Loading.IPlayables;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.Factories.ISingleItemPlayables;
using Zargess.VHKPlayer.Utility;

namespace Zargess.VHKPlayer.Test {
    /// <summary>
    /// Summary description for SingleItemPlayableTest
    /// </summary>
    [TestClass]
    public class SingleItemPlayableTest {
        private IPlayable _single;
        private IFile _testFile;
        [TestInitialize]
        public void Setup() {
            var folder = Constants.GetRootFolder();
            IFile file = new FileNode(PathHandler.CombinePaths(folder.FullPath, @"musik\andet\muse_2.mp3"));
            _single = new SingleItemPlayable(new SingleItemPlayableFactory(file));
            _testFile = new FileNode(@"C:\Users\MFH\vhk\musik\Andet\Test.txt");
        }

        [TestMethod]
        public void SingleItemPlayableContentSizeShouldBeOneAfterConstruction() {
            Assert.AreEqual(1, _single.Content.Count);
        }

        [TestMethod]
        public void SingleItemPlayableCannotAddTest_txtToItAndShouldHaveNoFilesInContent() {
            IPlayable single = new SingleItemPlayable(new SingleItemPlayableFactory(_testFile));
            Assert.AreEqual(0, single.Content.Count);
        }

        [TestMethod]
        public void SingleItemPlayableNameShouldBemuse_2_mp3() {
            Assert.AreEqual("muse_2.mp3", _single.Name);
        }

        [TestMethod]
        public void SingleItemPlayableNameShouldBeNullWhenFileDoesntExist() {
            IPlayable single = new SingleItemPlayable(new SingleItemPlayableFactory(_testFile));
            Assert.IsNull(single.Name);
        }

        [TestMethod]
        public void SingleItemPlayableCountOnQueueFromPlayCallShouldBe1() {
            Assert.AreEqual(1, _single.Play(PlayType.PlayList).Count);
        }

        [TestMethod]
        public void EmptySingleItemPlayablePlayCallShouldResultInEmptyQueue() {
            IPlayable single = new SingleItemPlayable(new SingleItemPlayableFactory(_testFile));
            Assert.AreEqual(0, single.Play(PlayType.PlayList).Count);
        }

        [TestMethod]
        public void SingleItemPlayableGetContentShouldReturn1Element() {
            Assert.AreEqual(1, _single.Content.Count);
        }
    }
}
