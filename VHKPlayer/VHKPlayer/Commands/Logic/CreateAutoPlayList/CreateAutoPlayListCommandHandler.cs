using System;
using VHKPlayer.Commands.Logic.CreatePlayList;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Utility;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Commands.Logic.CreateAutoPlayList
{
    class CreateAutoPlayListCommandHandler : ICommandHandler<CreateAutoPlayListCommand>
    {
        private readonly ICommandProcessor _processor;

        public CreateAutoPlayListCommandHandler(ICommandProcessor processor)
        {
            _processor = processor;
        }

        public void Handle(CreateAutoPlayListCommand command)
        {
            var playstrategy = (IPlayStrategy) Activator.CreateInstance(command.Command.PlayStrategy.GetType());
            playstrategy.Repeat = false;

            var playlistcommand = new CreatePlayListCommand()
            {
                Name = Constants.AutoPlayListName,
                Folder = command.Command.Folder,
                HasAudio = command.Command.HasAudio,
                LoadingStrategy = command.Command.LoadingStrategy,
                PlayStrategy = playstrategy
            };

            _processor.Process(playlistcommand);
        }
    }
}