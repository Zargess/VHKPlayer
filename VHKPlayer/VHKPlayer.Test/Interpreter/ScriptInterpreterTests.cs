using System.Collections.Generic;
using System.Linq;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Ploeh.AutoFixture;
using VHKPlayer.Interpreter.Interfaces;
using VHKPlayer.Models;
using VHKPlayer.Queries.GetFolders;
using VHKPlayer.Queries.GetStringSetting;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Test;

namespace VHKPlayer.Interpreter.Tests
{
    [TestClass()]
    public class ScriptInterpreterTests : TestBase
    {
        [TestMethod]
        public void TestEvaluateMultiSelector()
        {
            var intScript = "(property name:Number value:42)";
            var boolScript = "(property name:Trainer value:True)";
            var typeScript = "(type name:Player)";

            var script = new Script("(multi left:" + intScript + " right:(multi left:" + typeScript + " right:" +
                                    boolScript + "))");

            var player1 = new Player()
            {
                Trainer = true,
                Number = 42
            };
            var player2 = new Player()
            {
                Trainer = false,
                Number = 21
            };
            var player3 = new Player()
            {
                Trainer = true,
                Number = 21
            };
            var file = new FileNode().RandomizeTheRest();

            var container = CreateContainer();
            var interpreter = container.Resolve<IScriptInterpreter>();

            Assert.IsTrue(interpreter.Evaluate(script, player1));
            Assert.IsFalse(interpreter.Evaluate(script, player2));
            Assert.IsFalse(interpreter.Evaluate(script, player3));
            Assert.IsFalse(interpreter.Evaluate(script, file));
        }

        [TestMethod]
        public void TestEvaluatePropertySelector()
        {
            var path = @"test";
            var file1 = new FileNode()
            {
                FullPath = path
            }.RandomizeTheRest();
            var file2 = new FileNode()
            {
                FullPath = Fixture.Create<string>()
            }.RandomizeTheRest();
            var player1 = new Player()
            {
                Trainer = false,
                Number = 42
            };
            var player2 = new Player()
            {
                Trainer = true,
                Number = 21
            };

            var stringScript = new Script("(property name:FullPath value:\"" + path + "\")");
            var intScript = new Script("(property name:Number value:42)");
            var boolScript = new Script("(property name:Trainer value:True)");

            var container = CreateContainer();
            var interpreter = container.Resolve<IScriptInterpreter>();

            Assert.IsTrue(interpreter.Evaluate(stringScript, file1));
            Assert.IsFalse(interpreter.Evaluate(stringScript, file2));
            Assert.IsFalse(interpreter.Evaluate(stringScript, player1));

            Assert.IsTrue(interpreter.Evaluate(intScript, player1));
            Assert.IsFalse(interpreter.Evaluate(intScript, player2));
            Assert.IsFalse(interpreter.Evaluate(intScript, file1));

            Assert.IsTrue(interpreter.Evaluate(boolScript, player2));
            Assert.IsFalse(interpreter.Evaluate(boolScript, player1));
            Assert.IsFalse(interpreter.Evaluate(boolScript, file1));
        }

        [TestMethod]
        public void TestEvaluateTypeSelector()
        {
            var script = new Script("(type name:FileNode)");
            var container = CreateContainer();
            var interpreter = container.Resolve<IScriptInterpreter>();

            Assert.IsTrue(interpreter.Evaluate(script, new FileNode()));
            Assert.IsFalse(interpreter.Evaluate(script, new FolderNode(null)));
        }

        [TestMethod()]
        public void TestEvaluateFolderSelector()
        {
            var script = new Script("(folder path:\"root\\blandet\")");
            var root = Fixture.Create<string>();
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
                    FullPath = Fixture.Create<string>()
                }
            };

            var container = CreateContainer(c =>
            {
                c.RegisterFake<IQueryHandler<GetStringSettingQuery, string>>()
                    .Handle(Arg.Any<GetStringSettingQuery>())
                    .Returns(root);

                c.RegisterFake<IQueryHandler<GetFoldersQuery, IQueryable<FolderNode>>>()
                    .Handle(Arg.Any<GetFoldersQuery>())
                    .Returns(new[]
                    {
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