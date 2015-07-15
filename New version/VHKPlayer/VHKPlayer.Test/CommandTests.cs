using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VHKPlayer.Utility.FindFileType.Interfaces;
using NSubstitute;
using VHKPlayer.Models;
using Autofac;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Commands.Logic.CreateFile;

namespace VHKPlayer.Test
{
    /// <summary>
    /// Summary description for CommandTests
    /// </summary>
    [TestClass]
    public class CommandTests : TestBase
    {
        [TestMethod]
        public void TestCreateFolderStructure()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void TestCreatePlayableFile()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void TestCreateFolderNode()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void TestCreatePlayList()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void TestCreatePlayer()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void TestCreateFile()
        {
            //path
            var path = "c:\\test\\myfile.avi";

            var container = CreateContainer(c =>
            {
                c.RegisterFake<IFindFileTypeStrategy>()
                    .FindType(Arg.Any<string>())
                    .Returns(FileType.Video);
            });

            var processor = container.Resolve<ICommandProcessor>();

            var folder = new FolderNode(processor)
            {
                FullPath = "c:\\test"
            };

            processor.Process(new CreateFileCommand()
            {
                Folder = folder,
                Path = path
            });

            Assert.IsNotNull(folder.Content);
            Assert.AreEqual(1, folder.Content.Count);
        }
    }
}
