using System.Linq;
using VHKPlayer.Commands.Logic.CreateAutoPlayList;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Queries.GetStringSetting;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Queries.ParsePlayListString;
using VHKPlayer.Utility;

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

            var autoListName = _qprocessor.Process(new GetStringSettingQuery()
            {
                SettingName = Constants.AutoPlayListSettingName
            });

            var autoListCommand = commands.FirstOrDefault(x => x.Name == autoListName);

            if (autoListCommand == null) return; // TODO : Inform user?

            _cprocessor.Process(new CreateAutoPlayListCommand()
            {
                Command = autoListCommand
            });
        }
    }
}