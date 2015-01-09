﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VHKPlayer.ViewModels;
using VHKPlayer.Utility;
using VHKPlayer.Interfaces;
using VHKPlayer.Models;
using VHKPlayer.Test.Utility;
using VHKPlayer.Enums;

namespace VHKPlayer.Test.Models {
    /// <summary>
    /// Summary description for SinglePlayableTest
    /// </summary>
    [TestClass]
    public class SinglePlayableTest {
        IFile _file;
        IPlayable _playable;
        [TestInitialize]
        public void Setup() {
            var videoplayer = new VideoPlayer(new FolderSettings());
            _file = new FileNode(Constants.RootFolderPath + @"\Logo.png");
            _playable = new SinglePlayable(_file);
        }

        [TestMethod]
        public void SinglePlayableTestNameShouldBeLogo() {
            Assert.AreEqual("Logo.png", _playable.Name);
        }

        [TestMethod]
        public void SinglePlayableTestRepeatShouldBeFalse() {
            Assert.IsFalse(_playable.Repeat);
        }

        [TestMethod]
        public void SinglePlayableTestPlayCallShouldReturnQueueWith1File() {
            Assert.AreEqual(1, _playable.Play(PlayType.Music).Count);
        }
    }
}