package com.vhkplayer.queries;

public interface QueryHandler<TQuery extends Query<TResult>, TResult> {
    TResult handle(TQuery query);
}
