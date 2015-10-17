using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.CreatePlayList;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Infrastructure;
using VHKPlayer.Queries.GetFolderByPathSubscript;
using VHKPlayer.Queries.GetStringSetting;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Queries.ParsePlayListString;
using VHKPlayer.Utility;
using VHKPlayer.Utility.LoadingStrategy.PlayListLoading;
using VHKPlayer.Utility.PlayStrategy;

namespace VHKPlayer.Commands.Logic.CreateAllPlayLists
{
    class CreateAllPlayListsCommandHandler : ICommandHandler<CreateAllPlayListsCommand>
    {
        private readonly ICommandProcessor cprocessor;
        private readonly IQueryProcessor qprocessor;

        public CreateAllPlayListsCommandHandler(ICommandProcessor cprocessor, IQueryProcessor qprocessor)
        {
            this.cprocessor = cprocessor;
            this.qprocessor = qprocessor;
        }

        public void Handle(CreateAllPlayListsCommand com)
        {
            var constructString = qprocessor.Process(new GetStringSettingQuery()
            {
                SettingName = Constants.PlayListsSettingName
            });

            var commands = qprocessor.Process(new ParsePlayListStringQuery()
            {
                ConstructString = constructString
            }).ToList();

            foreach (var command in commands)
            {
                cprocessor.Process(command);
            }
        }
    }
}
