package com.vhkplayer.dao.commands;

public interface CommandHandlerCreationStrategy<TCommand extends Command> {
    CommandHandler<TCommand> createHandler();
}
