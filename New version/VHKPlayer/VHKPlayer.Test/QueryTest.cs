using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VHKPlayer.Models;
using VHKPlayer.Queries.GetPlayableFiles;
using VHKPlayer.Queries.Interfaces;
using System.Linq;
using NSubstitute;
using Autofac;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Queries.GetPlayablesAffectedByFolder;
using VHKPlayer.Queries.GetPlayerFolders;
using VHKPlayer.Queries.GetFolderByPathSubscript;
using VHKPlayer.Queries.IsStatFile;
using VHKPlayer.Queries.GetFolders;
using VHKPlayer.Queries.GetStringSetting;
using System.Collections.ObjectModel;
using VHKPlayer.Utility.FindFileType.Interfaces;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Test
{
    /// <summary>
    /// Summary description for QueryTest
    /// </summary>
    [TestClass]
    public class QueryTest : TestBase
    {
        [TestMethod]
        public void TestGetPlayableFiles()
        {

        }

        [TestMethod]
        public void TestGetFolders()
        {
            var path1 = "c:\\test1";
            var path2 = "c:\\test2";
            var path3 = "c:\\test3";

            var container = CreateContainer(c =>
            {
                c.RegisterFake<IDataCenter>()
                    .Folders
                    .Returns(new ObservableCollection<FolderNode>()
                    {
                        new FolderNode(null)
                        {
                            FullPath = path1
                        },
                        new FolderNode(null)
                        {
                            FullPath = path2
                        },
                        new FolderNode(null)
                        {
                            FullPath = path3
                        }
                    });
            });

            var processor = container.Resolve<IQueryProcessor>();

            var folders = processor.Process(new GetFoldersQuery());

            Assert.IsNotNull(folders);

            Assert.AreEqual(3, folders.Count());

            Assert.IsTrue(folders.Any(x => x.FullPath == path1));
            Assert.IsTrue(folders.Any(x => x.FullPath == path2));
            Assert.IsTrue(folders.Any(x => x.FullPath == path3));
        }

        [TestMethod]
        public void TestGetPlayerFolders()
        {
            var root = @"c:\test";

            var container = CreateContainer(c =>
            {
                c.RegisterFake<IQueryHandler<GetFoldersQuery, IQueryable<FolderNode>>>()
                    .Handle(Arg.Any<GetFoldersQuery>())
                    .Returns(new[]
                    {
                        new FolderNode(null)
                        {
                            FullPath = root + "Spiller"
                        },
                        new FolderNode(null)
                        {
                            FullPath = root + "SpillerVideo"
                        },
                        new FolderNode(null)
                        {
                            FullPath = root + "SpillerVideoStat"
                        },
                        new FolderNode(null)
                        {
                            FullPath = root + "SpillerVideoStat\\mp3"
                        },
                        new FolderNode(null)
                        {
                            FullPath = root + "SpillerVideoStat\\Video"
                        },
                        new FolderNode(null).RandomizeTheRest(),
                        new FolderNode(null).RandomizeTheRest()
                    }.AsQueryable());
            });

            var processor = container.Resolve<IQueryProcessor>();

            var folders = processor.Process(new GetPlayerFoldersQuery());

            Assert.IsNotNull(folders);

            Assert.AreEqual(5, folders.Count());
        }

        [TestMethod]
        public void TestGetFolderByPathSubscript()
        {
            var folderpath = "c:\\test\\humus\\carl";
            var subscript = "humus\\carl";

            var container = CreateContainer(c => 
            {
                c.RegisterFake<IQueryHandler<GetFoldersQuery, IQueryable<FolderNode>>>()
                    .Handle(Arg.Any<GetFoldersQuery>())
                    .Returns(new[]
                    {
                        new FolderNode(null)
                        {
                            FullPath = folderpath
                        },
                        new FolderNode(null)
                        {
                            FullPath = folderpath + "\\damn"
                        }
                    }.AsQueryable());
            });

            var processor = container.Resolve<IQueryProcessor>();

            var folder = processor.Process(new GetFolderByPathSubscriptQuery()
            {
                PartialPath = subscript
            });

            Assert.IsNotNull(folder);

            Assert.AreEqual(folderpath, folder.FullPath);
        }

        [TestMethod]
        public void TestIsStatFile()
        {
            var folderpath = "c:\\test";
            var file1 = new FileNode()
            {
                FullPath = folderpath + "\\test.png"
            };

            var file2 = new FileNode()
            {
                FullPath = "z:\\test\\test.png"
            };

            var container = CreateContainer(c =>
            {
                c.RegisterFake<IQueryHandler<GetFolderByPathSubscriptQuery, FolderNode>>()
                    .Handle(Arg.Any<GetFolderByPathSubscriptQuery>())
                    .Returns(new FolderNode(null)
                    {
                        FullPath = folderpath,
                        Content = new List<FileNode>()
                        {
                            file1
                        }
                    });
            });

            var processor = container.Resolve<IQueryProcessor>();

            var result1 = processor.Process(new IsStatFileQuery()
            {
                File = file1
            });

            var result2 = processor.Process(new IsStatFileQuery()
            {
                File = file2
            });

            Assert.IsTrue(result1);
            Assert.IsFalse(result2);
        }

        [TestMethod]
        public void TestGetPlayablesAffectedByFolder()
        {
            var folderPath = "c:\\test";
            var file = new FileNode()
            {
                FullPath = folderPath + "\\test.mp3"
            };

            var container = CreateContainer(c =>
            {
                c.RegisterFake<IQueryHandler<GetPlayableFilesQuery, IQueryable<PlayableFile>>>()
                    .Handle(Arg.Any<GetPlayableFilesQuery>())
                    .Returns(new[]
                    {
                        new PlayableFile()
                        {
                            File = file
                        }
                    }.AsQueryable());

                c.RegisterFake<IQueryHandler<GetPlayerFoldersQuery, IQueryable<FolderNode>>>()
                    .Handle(Arg.Any<GetPlayerFoldersQuery>())
                    .Returns(new[] 
                    {
                        new FolderNode(null)
                        {
                            FullPath = folderPath
                        }.RandomizeTheRest()
                    }.AsQueryable());
            });

            var commandProcessor = container.Resolve<ICommandProcessor>();

            var folder = new FolderNode(commandProcessor)
            {
                FullPath = folderPath
            };

            folder.AddFile(file);

            var queryProcessor = container.Resolve<IQueryProcessor>();

            var center = container.Resolve<DataCenter>();

            var playables = queryProcessor.Process(new GetPlayablesAffectedByFolderQuery()
            {
                Folder = folder
            });

            Assert.IsNotNull(playables);
            Assert.AreEqual(1, playables.Count());
        }
    }
}
