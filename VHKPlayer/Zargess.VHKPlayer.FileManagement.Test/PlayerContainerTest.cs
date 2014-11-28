﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Zargess.VHKPlayer.FileManagement.Interfaces;
using Zargess.VHKPlayer.SettingsManager;
using Zargess.VHKPlayer.UtilFunctions;

namespace Zargess.VHKPlayer.FileManagement.Test {
    /// <summary>
    /// Summary description for PlayerContainerTest
    /// </summary>
    [TestClass]
    public class PlayerContainerTest {
        IContainer _container;
        IFolder _folder;
        string path;
        [TestInitialize]
        public void Setup() {
            var path = @"D:\Github";
            if (Directory.Exists(path)) {
                _folder = new FolderNode(@"D:\Dropbox\Programmering\C#\damer 2013-2014");
            } else {
                _folder = new FolderNode(@"c:\users\mfh\vhk");
            }
            SettingsManagement.Instance.SetSetting("root", _folder.FullPath);
            this.path = PathHandler.AbsolutePath(SettingsManagement.Instance.GetPathSetting("playerFolders", 0));
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