package com.vhkplayer.bridge;

import com.vhkplayer.models.FileNode;
import netscape.javascript.JSObject;

import java.util.logging.Level;
import java.util.logging.Logger;

public class JavaBridge {
    private Logger logger = Logger.getLogger(Logger.GLOBAL_LOGGER_NAME);
    private Observable<FileNode> testObserver = new Observable<>();

    public JavaBridge() {
        logger.setLevel(Level.INFO);
    }

    public FileNode getNode() {
        return new FileNode("Bob");
    }

    public Subscription test(JSObject v) {
        return testObserver.subscribe(v);
    }

    public void debug(String msg) {
        logger.fine(msg);
        System.out.println(msg);
    }

    public void log(String msg) {
        logger.info(msg);
    }

    public void warn(String msg) {
        logger.warning(msg);
    }

    public void error(String msg) {
        logger.severe(msg);
    }

    public void setTestObject(FileNode test) {
        testObserver.notify(test);
    }
}
