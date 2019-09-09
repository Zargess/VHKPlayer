package com.vhkplayer.commands;

public interface CommandHandlerCreationStrategy<TCommand extends Command> {
    CommandHandler<TCommand> createHandler();
}
