package com.vhkplayer.bridge;

import netscape.javascript.JSObject;

import java.util.ArrayList;
import java.util.List;

public class Observable<T> {
    private List<JSObject> observers = new ArrayList<>();

    public void notify(T value) {
        observers.forEach(observer -> observer.call("notify", value));
    }

    public Subscription subscribe(JSObject observer) {
        this.observers.add(observer);
        return new Subscription(this::remove, observer);
    }

    private void remove(JSObject observer) {
        observers.remove(observer);
    }
}
