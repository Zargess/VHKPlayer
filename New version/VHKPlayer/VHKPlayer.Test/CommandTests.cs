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
using VHKPlayer.Utility.IsValidRootFolder.Interfaces;

namespace VHKPlayer.Test
{
    /// <summary>
    /// Summary description for CommandTests
    /// </summary>
    [TestClass]
    public class CommandTests : TestBase
    {
        [TestMethod]
        public void CreateFolderStructure()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void CreatePlayableFile()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void CreateFolderNode()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void CreatePlayList()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void CreatePlayer()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void CreateFile()
        {
            //path
            var path = "c:\\test\\myfile.avi";

            var container = CreateContainer(c =>
            {
                c.RegisterFake<IFindFileTypeStrategy>()
                    .FindType(Arg.Any<string>())
                    .Returns(FileType.Video);

                c.RegisterFake<IValidRootFolderStrategy>()
                    .IsValidRootFolder(Arg.Any<FolderNode>())
                    .Returns(false);
            });

            var isvalidrootstrategy = container.Resolve<IValidRootFolderStrategy>();

            var processor = container.Resolve<ICommandProcessor>();

            var folder = new FolderNode(processor)
            {
                FullPath = "c:\\test",
                ValidRootFolder = isvalidrootstrategy
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
