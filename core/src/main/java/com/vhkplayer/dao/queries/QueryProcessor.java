package com.vhkplayer.dao.queries;

import java.util.Map;
import java.util.Optional;

public class QueryProcessor {
    private final Map<Class, QueryHandlerCreationStrategy> handlerOptions;

    public QueryProcessor(Map<Class, QueryHandlerCreationStrategy> handlerOptions) {
        this.handlerOptions = handlerOptions;
    }

    public <TResult> Optional<TResult> process(Query<TResult> query) {
        QueryHandlerCreationStrategy<Query<TResult>, TResult> strategy = handlerOptions.get(query.getClass());
        if (strategy != null) {
            return Optional.of(strategy.createHandler().handle(query));
        }
        return Optional.empty();
    }
}
