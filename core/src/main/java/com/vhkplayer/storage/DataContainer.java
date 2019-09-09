package com.vhkplayer.storage;

import com.vhkplayer.folderstructure.FolderNode;

import java.util.ArrayList;
import java.util.List;

public class DataContainer {
    private static DataContainer instance;
    private List<FolderNode> folders = new ArrayList<>();

    private DataContainer() {}

    public void addFolder(FolderNode folder) {
        this.folders.add(folder);
        // TODO: Call update event
    }

    public List<FolderNode> getFolders() {
        return new ArrayList<>(folders);
    }

    public static DataContainer getInstance() {
        if (instance == null) {
            instance = new DataContainer();
        }
        return instance;
    }
}
