package com.vhkplayer.main;

import com.vhkplayer.bridge.JavaBridge;
import javafx.application.Application;
import javafx.scene.Group;
import javafx.scene.Scene;
import javafx.scene.layout.VBox;
import javafx.scene.paint.Color;
import javafx.scene.web.WebEngine;
import javafx.scene.web.WebView;
import javafx.stage.Stage;
import netscape.javascript.JSObject;

import java.net.URL;

public class StartFx extends Application {
    private static final String INDEX = "myapp://client/index.html";

    public void start(Stage stage) {
        URL.setURLStreamHandlerFactory(new MyURLStreamHandlerFactory());
        stage.setTitle("HTML");

        stage.setMaximized(true);
        Scene scene = new Scene(new Group(), Color.TRANSPARENT);
        VBox root = new VBox();

        final WebView webView = new WebView();
        webView.setPrefHeight(1080);

        final WebEngine webEngine = webView.getEngine();

        root.getChildren().add(webView);
        System.setProperty("sun.net.http.allowRestrictedHeaders", "true");
        webEngine.load(INDEX);
        scene.setRoot(root);

        stage.setScene(scene);
        stage.show();
        JSObject window = (JSObject) webEngine.executeScript("window");
        window.setMember("javabridge", new JavaBridge());
    }

    public void log(String msg) {
        System.out.println(msg);
    }

    public static void main(String[] args) {
        launch(args);
    }

    @Override
    public void stop() {
        System.out.println("exiting");
    }
}
