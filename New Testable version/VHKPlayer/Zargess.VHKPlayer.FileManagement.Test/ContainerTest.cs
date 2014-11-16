using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Zargess.VHKPlayer.FileManagement.Test {
    /// <summary>
    /// Summary description for ContainerTest
    /// </summary>
    [TestClass]
    public class ContainerTest {
        IContainer _container;
        IFolder _folder;
        [TestInitialize]
        public void Setup() {
            var path = @"D:\Github";
            if (Directory.Exists(path)) {
                _folder = new FolderNode(@"D:\Dropbox\Programmering\C#\damer 2013-2014");
            } else {
                _folder = new FolderNode(@"c:\users\mfh\vhk");
            }
            _container = new Container(new FolderNode(_folder.FullPath + @"\musik\Andet"));
        }

        [TestMethod]
        public void ContainerNameIsAndet() {
            _container = new Container(new FolderNode(_folder.FullPath + @"\musik\Andet"));
            Assert.AreEqual("Andet", _container.Name);
        }

        [TestMethod]
        public void ContainerNameIsScor() {
            _container = new Container(new FolderNode(_folder.FullPath + @"\musik\Scor"));
            Assert.AreEqual("Scor", _container.Name);
        }

        [TestMethod]
        public void AfterCreationContainerContentIsNotNull() {
            Assert.IsNotNull(_container.Content);
        }
    }
}
