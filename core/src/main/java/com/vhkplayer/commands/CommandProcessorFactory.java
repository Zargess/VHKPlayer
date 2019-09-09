package com.vhkplayer.commands;

import com.vhkplayer.commands.createallfolders.CreateAllFoldersCommand;
import com.vhkplayer.commands.createallfolders.CreateAllFoldersCommandCreationStrategy;
import com.vhkplayer.storage.DataContainer;

import java.util.HashMap;
import java.util.Map;

class CommandProcessorFactory {
    static CommandProcessor createCommandProcessor() {
        DataContainer container = DataContainer.getInstance();

        Map<Class, CommandHandlerCreationStrategy> handlerOptions = new HashMap<>();

        handlerOptions.put(CreateAllFoldersCommand.class, new CreateAllFoldersCommandCreationStrategy(container));

        return new CommandProcessor(handlerOptions);
    }
}
