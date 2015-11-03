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

namespace VHKPlayer.Interpreter.Tests
{
    [TestClass()]
    public class ScriptInterpreterTests : TestBase
    {
        [TestMethod]
        public void TestEvaluateTypeSelector()
        {
            var script = "(type FileNode)";
            var container = CreateContainer();
            var interpreter = container.Resolve<IScriptInterpreter>();

            Assert.IsTrue(interpreter.Evaluate(script, new FileNode()));
            Assert.IsFalse(interpreter.Evaluate(script, new FolderNode(null)));
        }

        [TestMethod()]
        public void TestEvaluateFolderSelector()
        {
            var script = "(folder \"root\\blandet\")";
            var file = new FileNode()
            {
                FullPath = fixture.Create<string>()
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
                c.RegisterFake<IQueryHandler<GetFolderByRelativePathQuery, FolderNode>>()
                    .Handle(Arg.Any<GetFolderByRelativePathQuery>())
                    .Returns(
                        new FolderNode(null)
                        {
                            Content = new List<FileNode>()
                            {
                                file
                            }
                        });
            });

            var interpreter = container.Resolve<IScriptInterpreter>();

            Assert.IsTrue(interpreter.Evaluate(script, playablefile1));
            Assert.IsFalse(interpreter.Evaluate(script, playablefile2));
        }
    }
}