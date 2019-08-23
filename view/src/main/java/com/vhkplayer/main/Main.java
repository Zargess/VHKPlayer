package com.vhkplayer.main;

import com.vhkplayer.folderstructure.FolderWatcherService;

import java.io.IOException;
import java.nio.file.Paths;

public class Main {
    public static void main(String[] args) throws IOException {
        System.out.println("WAT");
        var watcher = new FolderWatcherService(Paths.get("C:\\Users\\costa\\Downloads"));
        watcher.start();
        StartFx.main(args);
    }
}
