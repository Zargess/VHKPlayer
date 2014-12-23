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
    /// Summary description for PlayerContainerTest
    /// </summary>
    [TestClass]
    public class PlayerContainerTest {
        IContainer<IPlayable> _container;
        IFolder _folder;
        string path;
        [TestInitialize]
        public void Setup() {
            _folder = Constants.GetRootFolder();
            App.ConfigService.Update("root", _folder.FullPath);
            this.path = PathHandler.AbsolutePath(App.ConfigService.GetPathString("playerFolders", 0));
            _container = new PlayerContainer("Spiller", new FolderNode(this.path));
        }

        [TestMethod]
        public void ContainerContentShouldHaveSize22() {
            Assert.AreEqual(22, _container.Content.Count);
        }

        [TestMethod]
        public void ContainerNameShouldBeSpiller() {
            Assert.AreEqual("Spiller", _container.Name);
        }
    }
}
