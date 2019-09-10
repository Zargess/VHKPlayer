package com.vhkplayer.dao.commands.createallfolders;

import com.vhkplayer.dao.DataContainer;
import com.vhkplayer.dao.commands.CommandHandlerCreationStrategy;

public class CreateAllFoldersCommandHandlerCreationStrategy implements CommandHandlerCreationStrategy<CreateAllFoldersCommand> {
    private DataContainer container;

    public CreateAllFoldersCommandHandlerCreationStrategy(DataContainer container) {
        this.container = container;
    }

    @Override
    public CreateAllFoldersCommandHandler createHandler() {
        return new CreateAllFoldersCommandHandler(container);
    }
}
