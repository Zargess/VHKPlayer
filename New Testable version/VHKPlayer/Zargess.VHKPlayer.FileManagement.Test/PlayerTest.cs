using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Zargess.VHKPlayer.UtilFunctions;
using Zargess.VHKPlayer.FileManagement.Interfaces;

namespace Zargess.VHKPlayer.FileManagement.Test {
    /// <summary>
    /// Summary description for PlayerTest
    /// </summary>
    [TestClass]
    public class PlayerTest {
        IFolder _folder;
        IFile _file;
        IPlayer _player;

        [TestInitialize()]
        public void BeforeTests() {
            var path = @"D:\Github";
            if (Directory.Exists(path)) {
                Environment.CurrentDirectory = @"D:\GitHub";
                _folder = new FolderNode(@"D:\Dropbox\Programmering\C#\damer 2013-2014\Spiller");
            } else {
                Environment.CurrentDirectory = @"C:\Users\MFH\Documents\GitHub";
                _folder = new FolderNode(@"c:\users\mfh\vhk\spiller");
            }
            _file = new FileNode(PathHandler.CombinePaths(_folder.FullPath, @"\001 - Chana de Souza Mason.png"));
            _player = new Player();
        }

        [TestMethod]
        public void PlayerNumberShouldBe1() {
            
        }
    }
}