package com.vhkplayer.dao.commands.createallfolders;

import com.vhkplayer.dao.commands.Command;

import java.nio.file.Path;

public class CreateAllFoldersCommand implements Command {
    private Path path;

    public CreateAllFoldersCommand(Path path) {
        this.path = path;
    }

    public Path getPath() {
        return path;
    }
}
