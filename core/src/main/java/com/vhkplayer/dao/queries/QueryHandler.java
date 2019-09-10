package com.vhkplayer.dao.queries;

public interface QueryHandler<TQuery extends Query<TResult>, TResult> {
    TResult handle(TQuery query);
}
