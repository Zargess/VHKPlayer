package com.vhkplayer.dao;

import com.vhkplayer.dao.commands.CommandHandlerCreationStrategy;
import com.vhkplayer.dao.commands.CommandProcessor;
import com.vhkplayer.dao.commands.createallfolders.CreateAllFoldersCommand;
import com.vhkplayer.dao.commands.createallfolders.CreateAllFoldersCommandHandlerCreationStrategy;

import java.util.HashMap;
import java.util.Map;

class CommandProcessorFactory {
    static CommandProcessor createCommandProcessor(DataContainer container) {
        Map<Class, CommandHandlerCreationStrategy> handlerOptions = new HashMap<>();

        handlerOptions.put(CreateAllFoldersCommand.class, new CreateAllFoldersCommandHandlerCreationStrategy(container));

        return new CommandProcessor(handlerOptions);
    }
}
