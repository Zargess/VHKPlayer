using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Zargess.VHKPlayer.FileManagement.Interfaces;
using Zargess.VHKPlayer.FileManagement.Strategies.Loading.Container.IPlayLists;
using Zargess.VHKPlayer.SettingsManager;

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
            SettingsManagement.Instance.SetSetting("root", _folder.FullPath);
            _container = new PlayListContainer(new PlayListContainerLoadingStrategy());
        }

        [TestMethod]
        public void AfterCreationContainerContentIsNotNull() {
            Assert.IsNotNull(_container.Content);
        }

        [TestMethod]
        public void AfterLoadIsCalledPlayListContainerSizeShouldBeX() {
            _container.Load();
            Assert.AreEqual(3, _container.Content.Count);
        }

        [TestMethod]
        public void PlayListContainerShouldBePlayLister() {
            Assert.AreEqual("PlayLister", _container.Name);
        }
    }
}