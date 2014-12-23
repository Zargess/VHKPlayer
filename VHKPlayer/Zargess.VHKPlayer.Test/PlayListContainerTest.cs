﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zargess.VHKPlayer.Interfaces;
using System.IO;
using Zargess.VHKPlayer.Model;
using Zargess.VHKPlayer.Strategies.Loading.IContainers;

namespace Zargess.VHKPlayer.Test {
    /// <summary>
    /// Summary description for PlayListContainerTest
    /// </summary>
    [TestClass]
    public class PlayListContainerTest {
        IContainer<IPlayable> _container;
        IFolder _folder;
        [TestInitialize]
        public void Setup() {
            _folder = Constants.GetRootFolder();
            _container = new PlayListContainer(new PlayListContainerLoadingStrategy());
        }

        [TestMethod]
        public void AfterCreationContainerContentIsNotNull() {
            Assert.IsNotNull(_container.Content);
        }

        [TestMethod]
        public void AfterLoadIsCalledPlayListContainerSizeShouldBeX() {
            _container.Load();
            Assert.AreEqual(4, _container.Content.Count);
        }

        [TestMethod]
        public void PlayListContainerShouldBePlayLister() {
            Assert.AreEqual("PlayLister", _container.Name);
        }
    }
}
