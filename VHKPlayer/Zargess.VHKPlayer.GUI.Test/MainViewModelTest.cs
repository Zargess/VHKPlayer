using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Zargess.VHKPlayer.GUI.ViewModels;
using Zargess.VHKPlayer.FileManagement.Interfaces;
using Zargess.VHKPlayer.SettingsManager;
using Zargess.VHKPlayer.FileManagement;

namespace Zargess.VHKPlayer.GUI.Test {
    /// <summary>
    /// Summary description for MainViewModelTest
    /// </summary>
    [TestClass]
    public class MainViewModelTest {
        private MainViewModel _vm;
        private IFolder _folder;

        [TestInitialize()]
        public void BeforeTests() {
            var path = @"D:\Github";
            if (Directory.Exists(path)) {
                _folder = new FolderNode(@"D:\Dropbox\Programmering\C#\damer 2013-2014");
            } else {
                _folder = new FolderNode(@"c:\users\mfh\vhk");
            }
            SettingsManagement.Instance.SetSetting("root", _folder.FullPath);
            _vm = new MainViewModel(new VHKPlayerFactory());
        }

        [TestMethod]
        public void MusicContainerContentSizeShouldBeX() {
            Assert.AreEqual(9, _vm.MusicContainer.Content.Count);
        }

        [TestMethod]
        public void PlayerContainerContains22Players() {
            Assert.AreEqual(22, _vm.PlayerContainer.Content.Count);
        }

        [TestMethod]
        public void PlayListContainerContains12PlayLists() {
            Assert.AreEqual(3, _vm.PlayListContainer.Content.Count);
        }

        [TestMethod]
        public void CardContainerContains6Items() {
            Assert.AreEqual(8, _vm.CardContainer.Content.Count);
        }

        [TestMethod]
        public void MiscContainerContains4Items() {
            Assert.AreEqual(4, _vm.MiscContainer.Content.Count);
        }
    }
}
