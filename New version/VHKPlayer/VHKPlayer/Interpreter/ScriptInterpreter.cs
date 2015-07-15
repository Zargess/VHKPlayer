using System.Collections.Generic;
using VHKPlayer.Commands.Logic.CreatePlayList;
using VHKPlayer.Exceptions;
using VHKPlayer.Infrastructure;
using VHKPlayer.Models;
using VHKPlayer.Queries.GetFolderByPathSubscript;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility.LoadingStrategy.Interfaces;
using VHKPlayer.Utility.LoadingStrategy.PlayListLoading;
using VHKPlayer.Utility.PlayStrategy;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Interpreter
{
    public class ScriptInterpreter
    {
        private readonly IQueryProcessor processor;

        public ScriptInterpreter(IQueryProcessor processor)
        {
            this.processor = processor;
        }

        public CreatePlayListCommand ScriptToCreatePlayListCommand(string script)
        {
            var res = new CreatePlayListCommand();

            var temp = script.Split('@');

            var name = temp[1].Replace("Name='", "").Replace("'","");
            res.Name = name;

            var relativepath = temp[2].Replace("RelativePath=", "").Replace("'","").Replace("root\\","");
            var folder = processor.Process(new GetFolderByPathSubscriptQuery()
            {
                PartialPath = relativepath
            });
            res.Folder = folder;

            var repeat = temp[3].Replace("Repeat=", "").ToBool();

            var index = temp[5].Replace("Index=", "").ToInteger();

            var loadingStrategyString = temp[4].Replace("LoadMode=", "").Replace("'", "");
            var loadingStrategy = StringToLoadingStrategy(loadingStrategyString, folder, index);
            res.LoadingStrategy = loadingStrategy;

            var playStrategyString = temp[6].Replace("PlayMode=", "").Replace("'", "");
            var playStrategy = StringToPlayStrategy(playStrategyString);
            playStrategy.Repeat = repeat;
            res.PlayStrategy = playStrategy;

            return res;
        }

        private IPlayStrategy StringToPlayStrategy(string s)
        {
            if (s.Equals("AllFilesPlay"))
            {
                return new AllFilesPlayStrategy();
            } else if (s.Equals("IteratedPlay"))
            {
                return new IteratedPlayStrategy();
            } else
            {
                throw new SyntaxErrorException("Some illigal string was entered into the PlayMode: " + s);
            }
        }

        private ILoadingStrategy<ICollection<FileNode>> StringToLoadingStrategy(string s, FolderNode folder, int index)
        {
            if (s.Equals("FolderLoading"))
            {
                return new FolderLoadingStrategy(folder);
            } else if (s.Equals("SortedLoading"))
            {
                return new SortedLoadingStrategy(index, folder);
            } else
            {
                throw new SyntaxErrorException("Some illegal string was entered into the LoadingMode: " + s);
            }
        }
    }
}