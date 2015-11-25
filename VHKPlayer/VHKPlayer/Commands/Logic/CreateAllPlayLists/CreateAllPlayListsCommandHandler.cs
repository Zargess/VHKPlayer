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
        private readonly ICommandProcessor _cprocessor;
        private readonly IQueryProcessor _qprocessor;

        public CreateAllPlayListsCommandHandler(ICommandProcessor cprocessor, IQueryProcessor qprocessor)
        {
            this._cprocessor = cprocessor;
            this._qprocessor = qprocessor;
        }

        public void Handle(CreateAllPlayListsCommand com)
        {
            var constructString = _qprocessor.Process(new GetStringSettingQuery()
            {
                SettingName = Constants.PlayListsSettingName
            });

            var commands = _qprocessor.Process(new ParsePlayListStringQuery()
            {
                ConstructString = constructString
            }).ToList();

            foreach (var command in commands)
            {
                _cprocessor.Process(command);
            }
        }
    }
}
