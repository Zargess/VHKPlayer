using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zargess.VHKPlayer.Interfaces;
using System.IO;
using Zargess.VHKPlayer.Model;
using Zargess.VHKPlayer.Utility;

namespace Zargess.VHKPlayer.Test {
    /// <summary>
    /// Summary description for SingleItemContainerTest
    /// </summary>
    [TestClass]
    public class SingleItemContainerTest {
        IFolder _folder;
        IContainer<IPlayable> _container;
        [TestInitialize]
        public void Setup() {
            _folder = Constants.GetRootFolder();
            _container = new SingleItemContainer(new FolderNode(PathHandler.CombinePaths(_folder.FullPath, @"musik\andet")));
        }

        [TestMethod]
        public void SingleItemContainerNameShouldBeAndet() {
            Assert.AreEqual("andet", _container.Name);
        }

        [TestMethod]
        public void SingleItemContainerContentSizeShouldBeXAfterLoad() {
            _container.Load();
            Assert.AreEqual(26, _container.Content.Count);
        }
    }
}
