using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.ViewModels;
using System.IO;
using Zargess.VHKPlayer.Model;
using Zargess.VHKPlayer.Factories.ViewModels;

namespace Zargess.VHKPlayer.Test {
    /// <summary>
    /// Summary description for ViewModelTest
    /// </summary>
    [TestClass]
    public class ViewModelTest {
        private VideoPlayerViewModel _vm;
        private IFolder _folder;

        [TestInitialize()]
        public void BeforeTests() {
            var path = @"D:\Github";
            if (Directory.Exists(path)) {
                _folder = new FolderNode(@"D:\Dropbox\Programmering\C#\damer 2013-2014");
            } else {
                _folder = new FolderNode(@"c:\users\mfh\vhk");
            }
            App.ConfigService.Update("root", _folder.FullPath);
            _vm = new VideoPlayerViewModel(new VideoPlayerFactory());
        }

        [TestMethod]
        public void MusicContainerContentSizeShouldBeX() {
            Assert.AreEqual(10, _vm.MusicContainer.Content.Count);
        }

        [TestMethod]
        public void PlayerContainerContains22Players() {
            Assert.AreEqual(22, _vm.PlayerContainer.Content.Count);
        }

        [TestMethod]
        public void PlayListContainerContains12PlayLists() {
            Assert.AreEqual(4, _vm.PlayListContainer.Content.Count);
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
