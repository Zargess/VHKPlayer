package com.vhkplayer.dao.commands;

public interface CommandHandler<T extends Command> {
    void handle(T command);
}
