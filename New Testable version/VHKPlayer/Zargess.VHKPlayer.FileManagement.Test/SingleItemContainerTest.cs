using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Zargess.VHKPlayer.SettingsManager;
using Zargess.VHKPlayer.UtilFunctions;

namespace Zargess.VHKPlayer.FileManagement.Test {
    /// <summary>
    /// Summary description for SingleItemContainerTest
    /// </summary>
    [TestClass]
    public class SingleItemContainerTest {
        IFolder _folder;
        IContainer _container;
        [TestInitialize]
        public void Setup() {
            var path = @"D:\Github";
            if (Directory.Exists(path)) {
                _folder = new FolderNode(@"D:\Dropbox\Programmering\C#\damer 2013-2014");
            } else {
                _folder = new FolderNode(@"c:\users\mfh\vhk");
            }
            SettingsManagement.SetSetting("root", _folder.FullPath);
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
