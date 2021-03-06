﻿using System;
using System.Windows.Input;
using VHKPlayer.Controllers.Interfaces;
using VHKPlayer.Queries.GetPlayListByName;
using VHKPlayer.Queries.GetStringSetting;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility;

namespace VHKPlayer.Commands.GUI
{
    public class PlayAutoPlayListCommand : ICommand
    {
        private readonly IQueryProcessor _processor;
        private readonly IVideoPlayerController _controller;

        public event EventHandler CanExecuteChanged;

        public PlayAutoPlayListCommand(IQueryProcessor processor, IVideoPlayerController controller)
        {
            _processor = processor;
            _controller = controller;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var playlist = _processor.Process(new GetPlayListByNameQuery
            {
                Name = _processor.Process(new GetStringSettingQuery
                {
                    SettingName = Constants.AutoPlayListSettingName
                })
            });
            _controller.Play(playlist, playlist.PlayStrategy);
        }
    }
}