package com.vhkplayer.dao.commands;

import java.util.Map;

public final class CommandProcessor {
    private Map<Class, CommandHandlerCreationStrategy> handlerOptions;

    public CommandProcessor(Map<Class, CommandHandlerCreationStrategy> handlerOptions) {
        this.handlerOptions = handlerOptions;
    }

    public <TCommand extends Command> void process(TCommand command) {
        CommandHandlerCreationStrategy<TCommand> creationStrategy = handlerOptions.get(command.getClass());
        if (creationStrategy != null) {
            creationStrategy.createHandler().handle(command);
        }
    }
}
