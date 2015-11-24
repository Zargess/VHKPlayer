﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Commands.Logic.RemoveFolder
{
    class RemoveFolderCommandHandler : ICommandHandler<RemoveFolderCommand>
    {
        private readonly IDataCenter _center;

        public RemoveFolderCommandHandler(IDataCenter center)
        {
            this._center = center;
        }

        public void Handle(RemoveFolderCommand command)
        {
            command.Folder.Observers.ForEach(x => command.Folder.RemoveObserver(x));

            _center.Folders.Remove(command.Folder);
        }
    }
}
