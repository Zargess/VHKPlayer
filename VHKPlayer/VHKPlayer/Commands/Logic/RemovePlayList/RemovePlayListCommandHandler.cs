using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Commands.Logic.RemovePlayList
{
    class RemovePlayListCommandHandler : ICommandHandler<RemovePlayListCommand>
    {
        private readonly IDataCenter _center;

        public RemovePlayListCommandHandler(IDataCenter center)
        {
            this._center = center;
        }

        public void Handle(RemovePlayListCommand command)
        {
            _center.PlayLists.Remove(command.Playlist);
        }
    }
}
