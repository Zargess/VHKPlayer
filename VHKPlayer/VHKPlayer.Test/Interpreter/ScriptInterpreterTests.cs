using Microsoft.VisualStudio.TestTools.UnitTesting;
using VHKPlayer.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models;
using VHKPlayer.Test;
using VHKPlayer.Queries.GetFolderByRelativePath;
using VHKPlayer.Queries.Interfaces;
using NSubstitute;
using Ploeh.AutoFixture;
using Autofac;
using VHKPlayer.Interpreter.Interfaces;
using VHKPlayer.Queries.GetFolders;
using VHKPlayer.Queries.GetStringSetting;

namespace VHKPlayer.Interpreter.Tests
{
    [TestClass()]
    public class ScriptInterpreterTests : TestBase
    {
        [TestMethod]
        public void TestEvaluatePropertySelector()
        {
            var script = "TODO : Make a property selector script and test if it works!";
            var container = CreateContainer();
            var interpreter = container.Resolve<IScriptInterpreter>();

            Assert.IsTrue(interpreter.Evaluate(script, new FileNode()));
            Assert.IsFalse(interpreter.Evaluate(script, new FolderNode(null)));
        }

        [TestMethod]
        public void TestEvaluateTypeSelector()
        {
            var script = "(type name:FileNode)";
            var container = CreateContainer();
            var interpreter = container.Resolve<IScriptInterpreter>();

            Assert.IsTrue(interpreter.Evaluate(script, new FileNode()));
            Assert.IsFalse(interpreter.Evaluate(script, new FolderNode(null)));
        }

        [TestMethod()]
        public void TestEvaluateFolderSelector()
        {
            var script = "(folder path:\"root\\blandet\")";
            var root = fixture.Create<string>();
            var path = root + "\\blandet";
            var file = new FileNode()
            {
                FullPath = path + @"\test.png"
            };
            var playablefile1 = new PlayableFile()
            {
                File = file
            };
            var playablefile2 = new PlayableFile()
            {
                File = new FileNode()
                {
                    FullPath = fixture.Create<string>()
                }
            };

            var container = CreateContainer(c =>
            {
                c.RegisterFake<IQueryHandler<GetStringSettingQuery, string>>()
                    .Handle(Arg.Any<GetStringSettingQuery>())
                    .Returns(root);

                c.RegisterFake<IQueryHandler<GetFoldersQuery, IQueryable<FolderNode>>>()
                    .Handle(Arg.Any<GetFoldersQuery>())
                    .Returns(new[] {
                            new FolderNode(null)
                            {
                                FullPath = path,
                                Content = new List<FileNode>()
                                {
                                    file
                                }
                            }
                        }.AsQueryable());
            });

            var interpreter = container.Resolve<IScriptInterpreter>();

            Assert.IsTrue(interpreter.Evaluate(script, playablefile1));
            Assert.IsFalse(interpreter.Evaluate(script, playablefile2));
        }
    }
}