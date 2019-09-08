package com.vhkplayer.commands.createallfolders;

import com.vhkplayer.commands.CommandHandler;
import com.vhkplayer.storage.DataContainer;

public class CreateAllFoldersCommandHandler implements CommandHandler<CreateAllFoldersCommand> {
    private DataContainer container;

    public CreateAllFoldersCommandHandler(DataContainer container) {
        this.container = container;
    }

    @Override
    public void handle(CreateAllFoldersCommand command) {

    }
}
