using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Queries.Interfaces;

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

        public void Handle(CreateAllPlayListsCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
