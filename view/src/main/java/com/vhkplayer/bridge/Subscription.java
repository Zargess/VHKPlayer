package com.vhkplayer.bridge;

import netscape.javascript.JSObject;

import java.util.function.Consumer;

public class Subscription {
    private Consumer<JSObject> unsubscribeHandler;
    private JSObject observer;

    public Subscription(Consumer<JSObject> unsubscribeHandler, JSObject observer) {
        this.unsubscribeHandler = unsubscribeHandler;
        this.observer = observer;
    }

    public void unsubscribe() {
        unsubscribeHandler.accept(observer);
    }
}
