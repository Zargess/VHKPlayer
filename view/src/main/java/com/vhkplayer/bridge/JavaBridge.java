package com.vhkplayer.bridge;

import com.vhkplayer.models.FileNode;

public class JavaBridge {
    public FileNode getNode() {
        return new FileNode("Bob");
    }

    public void log(String msg) {
        System.out.println(msg);
    }
}
