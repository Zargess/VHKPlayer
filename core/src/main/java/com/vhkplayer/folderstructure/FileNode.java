package com.vhkplayer.folderstructure;

import com.google.common.io.Files;

import java.io.Serializable;
import java.nio.file.Paths;
import java.util.Objects;

public class FileNode implements Serializable {
    private final String path;

    public FileNode(String path) {
        this.path = path;
    }

    public boolean exists() {
        return java.nio.file.Files.exists(Paths.get(path));
    }

    public String getName() {
        return Files.getNameWithoutExtension(path);
    }

    public String getPath() {
        return path;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        FileNode fileNode = (FileNode) o;
        return Objects.equals(path, fileNode.path);
    }

    @Override
    public int hashCode() {
        return Objects.hash(path);
    }
}
