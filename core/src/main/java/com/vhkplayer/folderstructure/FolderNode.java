package com.vhkplayer.folderstructure;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;
import java.util.stream.Stream;

public class FolderNode {
    private final List<FileNode> files;
    private final FolderWatcherService watcher;
    private final Path path;
    private final List<FolderObserver> observers;

    public FolderNode(Path path) throws IOException {
        this.path = path;
        this.files = new ArrayList<>();
        createFiles();
        observers = new ArrayList<>();
        this.watcher = new FolderWatcherService(path, this::onFolderChange, this::onFolderChange);
    }

    public void addObserver(FolderObserver observer) {
        observers.add(observer);
    }

    public void removeObserver(FolderObserver observer) {
        observers.remove(observer);
    }

    public boolean contains(FileNode file) {
        return files.contains(file);
    }

    public boolean exists() {
        return Files.exists(path);
    }

    public void destroy() {
        this.watcher.stop();
    }

    public List<FileNode> getFiles() {
        return files;
    }

    private void createFiles() {
        if (!exists()) {
            destroy();
            return;
        }
        try (Stream<Path> walk = Files.walk(path)) {
            walk.filter(Files::isRegularFile)
                    .map(Path::toString)
                    .collect(Collectors.toList()).forEach(filePath -> files.add(new FileNode(filePath)));
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private void onFolderChange(Path filePath) {
        files.clear();
        createFiles();
        observers.forEach(observer -> observer.folderUpdated(this));
    }

    @Override
    public String toString() {
        return "FolderNode{" +
                " path=" + path +
                '}';
    }
}
