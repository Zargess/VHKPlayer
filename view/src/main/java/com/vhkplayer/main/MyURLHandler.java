package com.vhkplayer.main;

import java.net.URL;
import java.net.URLConnection;
import java.net.URLStreamHandler;

public class MyURLHandler extends URLStreamHandler {
    @Override
    protected URLConnection openConnection(URL url) {
        return new MyURLConnection(url);
    }
}
