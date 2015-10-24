using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.CreatePlayList;
using VHKPlayer.Infrastructure;
using VHKPlayer.Queries.GetFolderByPathSubscript;
using VHKPlayer.Queries.GetPlayListLoadingStrategy;
using VHKPlayer.Queries.GetPlayStrategy;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility.LoadingStrategy.PlayListLoading;
using VHKPlayer.Utility.PlayStrategy;

namespace VHKPlayer.Queries.ParsePlayListString
{
    class ParsePlayListStringQueryHandler : IQueryHandler<ParsePlayListStringQuery, IQueryable<CreatePlayListCommand>>
    {
        private readonly IQueryProcessor processor;

        public ParsePlayListStringQueryHandler(IQueryProcessor processor)
        {
            this.processor = processor;
        }

        // TODO : Make this an interpreter for a script language that defines playlists instead of makeing it this hardcoded
        public IQueryable<CreatePlayListCommand> Handle(ParsePlayListStringQuery query)
        {
            var playlistDefinitions = query.ConstructString.Split(',');

            var commands = new List<CreatePlayListCommand>();

            foreach (var def in playlistDefinitions)
            {
                var temp = def.Replace("{", "");
                temp = temp.Replace("}", "");

                var variables = temp.Split(';');

                var command = new CreatePlayListCommand();

                command.Name = variables[0];

                var partialPath = variables[1].Replace("root\\", "");


                var folder = processor.Process(new GetFolderByPathSubscriptQuery()
                {
                    PartialPath = partialPath
                });
                command.Folder = folder;

                var has_audio = variables[2].ToBool();
                command.HasAudio = has_audio;

                var index = variables[3].ToInteger();
                
                var loading = variables[4];

                command.LoadingStrategy = processor.Process(new GetPlayListLoadingStrategyQuery()
                {
                    Folder = folder,
                    Index = index,
                    StrategyName = loading
                });

                var playing = variables[5];

                command.PlayStrategy = processor.Process(new GetPlayStrategyQuery()
                {
                    StrategyName = playing
                });

                commands.Add(command);
            }

            return commands.AsQueryable();
        }
    }
}
