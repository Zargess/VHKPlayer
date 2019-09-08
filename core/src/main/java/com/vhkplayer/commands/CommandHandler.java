package com.vhkplayer.commands;

public interface CommandHandler<T extends Command> {
    void handle(T command);
}
