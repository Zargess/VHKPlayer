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
    public class PlayListContainerTest {
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
            _container = new PlayListContainer(new FolderNode(_folder.FullPath + @"\musik\Andet"));
        }

        [TestMethod]
        public void AfterCreationContainerContentIsNotNull() {
            Assert.IsNotNull(_container.Content);
        }
    }
}
