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
using System.IO;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Queries.GetPlayerFolders;
using System.Linq;
using VHKPlayer.Commands.Logic.CreatePlayer;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Test
{
    /// <summary>
    /// Summary description for CommandTests
    /// </summary>
    [TestClass]
    public class CommandTests : TestBase
    {
        [TestMethod]
        public void TestUpdateDataCenterByFolder()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void TestResetDataCenter()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void TestRemovePlayList()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void TestRemovePlayer()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void TestRemovePlayableFile()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void TestRemoveFolder()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void TestRemoveDataObserver()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void TestCreatePlayableFilesFromFilesInFolder()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void TestCreateAllPlayLists()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void TestCreateAllPlayers()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void TestCreateAllPlayables()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void TestCreateAllPlayableFiles()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void TestChangesSetting()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void TestAddDataObserver()
        {
            throw new NotImplementedException();
        }

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
            var filename = "001 - Chana de Souza Mason.png";
            var playername = "Chana de Souza Mason";
            var number = 1;
            var file = new FileNode()
            {
                Name = filename,
                NameWithoutExtension = "001 - Chana de Souza Mason",
                FullPath = Path.Combine("stuff", filename)
            };
            var statFolder = new FolderNode(null);

            var container = CreateContainer(c =>
            {
                c.RegisterFake<IQueryHandler<GetPlayerFoldersQuery, IQueryable<FolderNode>>>()
                    .Handle(Arg.Any<GetPlayerFoldersQuery>())
                    .Returns(new[]
                    {
                        new FolderNode(null)
                        {
                            Content = new List<FileNode>()
                            {
                                file
                            }
                        }
                    }.AsQueryable());
            });

            var processor = container.Resolve<ICommandProcessor>();
            processor.Process(new CreatePlayerCommand()
            {
                File = file,
                Folder = statFolder
            });

            var datacenter = container.Resolve<IDataCenter>();

            Assert.AreEqual(playername, datacenter.Players[0].Name);
            Assert.AreEqual(number, datacenter.Players[0].Number);
            Assert.AreEqual(file, datacenter.Players[0].Content.ToList()[0]);
            Assert.IsFalse(datacenter.Players[0].Trainer);
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
