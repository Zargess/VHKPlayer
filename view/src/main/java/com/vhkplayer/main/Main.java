package com.vhkplayer.main;

import java.io.File;

public class Main {
    public static void main(String[] args) {
        File file = new File(
                Main.class.getClassLoader().getResource("client/index.html").getFile()
        );
        System.out.println(file.getPath());
        System.out.println("Bob");
    }
}
