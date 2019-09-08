package com.vhkplayer.storage;

import com.vhkplayer.folderstructure.FolderNode;

import java.util.ArrayList;
import java.util.List;

public class DataContainer {
    private List<FolderNode> folders = new ArrayList<>();

    public void addFolder(FolderNode folder) {
        this.folders.add(folder);
        // TODO: Call update event
    }
}
