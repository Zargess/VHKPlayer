package com.vhkplayer.dao.commands.createallfolders;

import com.vhkplayer.dao.DataContainer;
import com.vhkplayer.dao.commands.CommandHandler;
import com.vhkplayer.folderstructure.FolderNode;

import java.io.File;
import java.io.IOException;
import java.nio.file.Path;
import java.util.logging.Level;
import java.util.logging.Logger;

public class CreateAllFoldersCommandHandler implements CommandHandler<CreateAllFoldersCommand> {
    private Logger logger = Logger.getLogger(Logger.GLOBAL_LOGGER_NAME);
    private DataContainer container;

    public CreateAllFoldersCommandHandler(DataContainer container) {
        this.container = container;
        logger.setLevel(Level.INFO);
    }

    @Override
    public void handle(CreateAllFoldersCommand command) {
        scanThroughFolder(command.getPath().toAbsolutePath().toString());
    }

    private void scanThroughFolder(String folderPath) {
        File[] files = new File(folderPath).listFiles();
        if (files == null) {
            logger.severe("Could not scan through folder: " + folderPath);
            return;
        }
        for (File file : files) {
            if (file.isDirectory()) {
                try {
                    container.addFolder(new FolderNode(Path.of(file.getAbsolutePath())));
                    scanThroughFolder(file.getAbsolutePath());
                } catch (IOException e) {
                    logger.warning("Could not load folder: " + file.getAbsolutePath());
                }
            }
        }
    }
}
