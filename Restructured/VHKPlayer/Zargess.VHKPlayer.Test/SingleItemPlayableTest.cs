﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.Model;
using Zargess.VHKPlayer.Strategies.Loading.IPlayables;
using Zargess.VHKPlayer.Enums;

namespace Zargess.VHKPlayer.Test {
    /// <summary>
    /// Summary description for SingleItemPlayableTest
    /// </summary>
    [TestClass]
    public class SingleItemPlayableTest {
        private IPlayable _single;
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
            Assert.AreEqual(1, _single.Content.Count);
        }

        [TestMethod]
        public void SingleItemPlayableCannotAddTest_txtToItAndShouldHaveNoFilesInContent() {
            IPlayable single = new SingleItemPlayable(new FileLoadingStrategy(@"C:\Users\MFH\vhk\musik\Andet\Test.txt"));
            Assert.AreEqual(0, single.Content.Count);
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

        [TestMethod]
        public void SingleItemPlayableGetContentShouldReturn1Element() {
            Assert.AreEqual(1, _single.Content.Count);
        }
    }
}
