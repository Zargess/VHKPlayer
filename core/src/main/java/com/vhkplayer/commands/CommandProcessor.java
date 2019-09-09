package com.vhkplayer.commands;

import java.util.Map;

public final class CommandProcessor {
    private static CommandProcessor instance;

    private Map<Class, CommandHandlerCreationStrategy> handlerOptions;

    CommandProcessor(Map<Class, CommandHandlerCreationStrategy> handlerOptions) {
        this.handlerOptions = handlerOptions;
    }

    public <TCommand extends Command> void process(TCommand command) {
        CommandHandlerCreationStrategy<TCommand> creationStrategy = handlerOptions.get(command.getClass());
        if (creationStrategy != null) {
            creationStrategy.createHandler().handle(command);
        }
    }

    public static CommandProcessor getInstance() {
        if (instance == null) {
            instance = CommandProcessorFactory.createCommandProcessor();
        }
        return instance;
    }
}
