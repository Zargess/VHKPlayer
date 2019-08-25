package com.vhkplayer.main;

import com.vhkplayer.bridge.JavaBridge;
import com.vhkplayer.models.FileNode;
import javafx.application.Application;
import javafx.application.Platform;
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
        stage.setTitle("VHK Player");

        stage.setMaximized(true);
        Scene scene = new Scene(new Group(), Color.TRANSPARENT);
        VBox root = new VBox();

        final WebView webView = new WebView();
        webView.prefWidthProperty().bind(stage.widthProperty());
        webView.prefHeightProperty().bind(stage.heightProperty());

        final WebEngine webEngine = webView.getEngine();
        root.getChildren().add(webView);
        System.setProperty("sun.net.http.allowRestrictedHeaders", "true");
        JSObject window = (JSObject) webEngine.executeScript("window");
        JavaBridge bridge = new JavaBridge();
        window.setMember("javabridge", bridge);
        webEngine.executeScript("console.debug = function(message)\n" +
                "{\n" +
                "    javabridge.debug(JSON.stringify(message));\n" +
                "};");
        webEngine.executeScript("console.log = function(message)\n" +
                "{\n" +
                "    javabridge.log(JSON.stringify(message));\n" +
                "};");
        webEngine.executeScript("console.warn = function(message)\n" +
                "{\n" +
                "    javabridge.warn(JSON.stringify(message));\n" +
                "};");
        webEngine.executeScript("console.error = function(message)\n" +
                "{\n" +
                "    javabridge.error(JSON.stringify(message));\n" +
                "};");
        scene.setRoot(root);

        stage.setScene(scene);
        webEngine.load(INDEX);
        stage.show();
        Thread thread = new Thread(() -> {
            try {
                Thread.sleep(5000);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
            Platform.runLater(() -> bridge.setTestObject(new FileNode("Cucko/bonks.wat")));
        });
        thread.start();
    }

    public static void main(String[] args) {
        launch(args);
    }

    @Override
    public void stop() {
        System.out.println("exiting");
    }
}
