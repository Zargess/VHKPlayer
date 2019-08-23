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
import java.util.HashMap;

import static java.nio.file.StandardWatchEventKinds.ENTRY_CREATE;
import static java.nio.file.StandardWatchEventKinds.ENTRY_DELETE;
import static java.nio.file.StandardWatchEventKinds.ENTRY_MODIFY;
import static java.nio.file.StandardWatchEventKinds.OVERFLOW;

public class FolderWatcherService {
    private final WatchService watcher;
    private final HashMap<WatchKey, Path> keys;
    private boolean running;
    private Thread thread;
    private Path path;

    public FolderWatcherService(Path path) throws IOException {
        this.path = path;
        this.watcher = FileSystems.getDefault().newWatchService();
        this.keys = new HashMap<>();
    }

    public void start() {
        if (!running) {
            this.running = true;
            registerAllDirectories(path);
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
            keys.clear();
        }
    }

    private void registerAllDirectories(Path root) {
        try {
            Files.walkFileTree(root, new SimpleFileVisitor<>() {
                @Override
                public FileVisitResult preVisitDirectory(Path dir, BasicFileAttributes attrs) {
                    registerDirectory(dir);
                    return FileVisitResult.CONTINUE;
                }

                @Override
                public FileVisitResult visitFile(Path file, BasicFileAttributes attrs) throws IOException {
                    System.out.println("Found: " + file.toAbsolutePath().toString());
                    return FileVisitResult.CONTINUE;
                }
            });
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private void registerDirectory(Path dir) {
        try {
            WatchKey key = dir.register(watcher, ENTRY_CREATE, ENTRY_DELETE);
            Path prev = keys.get(key);
            if (prev == null) {
                System.out.format("register: %s\n", dir);
            } else {
                if (!dir.equals(prev)) {
                    System.out.format("update: %s -> %s\n", prev, dir);
                }
            }
            keys.put(key, dir);
        } catch (IOException e) {
            e.printStackTrace();
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

            Path dir = keys.get(key);
            if (dir == null) {
                System.err.println("WatchKey not recognized");
                continue;
            }

            key.pollEvents().forEach(event -> {
                WatchEvent.Kind kind = event.kind();
                if (kind != OVERFLOW) {
                    Path name = (Path) event.context();
                    Path child = dir.resolve(name);

                    // print out event
                    System.out.format("%s: %s\n", event.kind().name(), child);

                    if (kind == ENTRY_CREATE) {
                        if (Files.isDirectory(child)) {
                            registerAllDirectories(child);
                        }
                    }
                }
            });
            boolean valid = key.reset();
            if (!valid) {
                keys.remove(key);
                if (keys.isEmpty()) {
                    return;
                }
            }
        }
    }
}
