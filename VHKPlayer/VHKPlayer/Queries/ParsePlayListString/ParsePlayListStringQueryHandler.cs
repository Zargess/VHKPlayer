using System.Collections.Generic;
using System.Linq;
using VHKPlayer.Commands.Logic.CreatePlayList;
using VHKPlayer.Infrastructure;
using VHKPlayer.Queries.GetFolderByPathSubscript;
using VHKPlayer.Queries.GetPlayListLoadingStrategy;
using VHKPlayer.Queries.GetPlayStrategy;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.ParsePlayListString
{
    class ParsePlayListStringQueryHandler : IQueryHandler<ParsePlayListStringQuery, IQueryable<CreatePlayListCommand>>
    {
        private readonly IQueryProcessor _processor;

        public ParsePlayListStringQueryHandler(IQueryProcessor processor)
        {
            this._processor = processor;
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


                var folder = _processor.Process(new GetFolderByPathSubscriptQuery()
                {
                    PartialPath = partialPath
                });
                command.Folder = folder;

                var hasAudio = variables[2].ToBool();
                command.HasAudio = hasAudio;

                var index = variables[3].ToInteger();

                var repeat = variables[4].ToBool();

                var loading = variables[5];

                command.LoadingStrategy = _processor.Process(new GetPlayListLoadingStrategyQuery()
                {
                    Folder = folder,
                    Index = index,
                    StrategyName = loading
                });

                var playing = variables[6];

                command.PlayStrategy = _processor.Process(new GetPlayStrategyQuery()
                {
                    StrategyName = playing
                });

                command.PlayStrategy.Repeat = repeat;

                commands.Add(command);
            }

            return commands.AsQueryable();
        }
    }
}