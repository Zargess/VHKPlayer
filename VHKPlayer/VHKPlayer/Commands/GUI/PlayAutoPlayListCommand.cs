using System;
using System.Windows.Input;
using VHKPlayer.Controllers.Interfaces;
using VHKPlayer.Queries.GetAutoPlayList;
using VHKPlayer.Queries.Interfaces;

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
            var playlist = _processor.Process(new GetAutoPlayListQuery());
            if (playlist == null) return;
            _controller.Play(playlist, playlist.PlayStrategy);
        }
    }
}