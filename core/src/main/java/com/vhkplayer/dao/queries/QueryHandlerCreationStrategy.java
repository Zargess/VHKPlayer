package com.vhkplayer.dao.queries;

public interface QueryHandlerCreationStrategy<TQuery extends Query<TResult>, TResult> {
    QueryHandler<TQuery, TResult> createHandler();
}
