package com.vhkplayer.commands.createallfolders;

import com.vhkplayer.commands.CommandHandlerCreationStrategy;
import com.vhkplayer.storage.DataContainer;

public class CreateAllFoldersCommandCreationStrategy implements CommandHandlerCreationStrategy<CreateAllFoldersCommand> {
    private DataContainer container;

    public CreateAllFoldersCommandCreationStrategy(DataContainer container) {
        this.container = container;
    }

    @Override
    public CreateAllFoldersCommandHandler createHandler() {
        return new CreateAllFoldersCommandHandler(container);
    }
}