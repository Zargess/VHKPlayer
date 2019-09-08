package com.vhkplayer.folderstructure;

import java.io.IOException;
import java.nio.file.FileSystems;
import java.nio.file.FileVisitResult;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.SimpleFileVisitor;
import java.nio.file.WatchEvent;
import java.nio.file.WatchKey;
import java.nio.file.WatchService;
import java.nio.file.attribute.BasicFileAttributes;
import java.util.function.Consumer;

import static java.nio.file.StandardWatchEventKinds.ENTRY_CREATE;
import static java.nio.file.StandardWatchEventKinds.ENTRY_DELETE;
import static java.nio.file.StandardWatchEventKinds.OVERFLOW;

public class FolderWatcherService {
    private final WatchService watcher;
    private boolean running;
    private Thread thread;
    private Path dir;
    private final Consumer<Path> onCreate;
    private final Consumer<Path> onDelete;

    public FolderWatcherService(Path dir, Consumer<Path> onCreate, Consumer<Path> onDelete) throws IOException {
        this.dir = dir;
        this.onCreate = onCreate;
        this.onDelete = onDelete;
        this.watcher = FileSystems.getDefault().newWatchService();
        dir.register(watcher, ENTRY_CREATE, ENTRY_DELETE);
    }

    public void start() {
        if (!running) {
            this.running = true;
            thread = new Thread(this::run);
            thread.setDaemon(true);
            thread.start();
        }
    }

    public void stop() {
        if (running) {
            running = false;
            thread.interrupt();
            try {
                watcher.close();
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
    }

    private void run() {
        while (running) {
            WatchKey key;
            try {
                key = watcher.take();
            } catch (InterruptedException e) {
                continue;
            }

            key.pollEvents().forEach(event -> {
                WatchEvent.Kind kind = event.kind();
                Path name = (Path) event.context();
                Path child = dir.resolve(name);
                if (kind != OVERFLOW && !Files.isDirectory(child)) {
                    System.out.format("%s: %s\n", event.kind().name(), child);
                    if (kind == ENTRY_CREATE) {
                        onCreate.accept(child);
                    } else if (kind == ENTRY_DELETE) {
                        onDelete.accept(child);
                    }
                }
            });
            boolean valid = key.reset();
            if (!valid) {
                stop();
            }
        }
    }
}
