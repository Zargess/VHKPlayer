package com.vhkplayer.queries;

import java.util.Map;
import java.util.Optional;

public class QueryProcessor {
    private static QueryProcessor instance;

    private final Map<Class, QueryHandlerCreationStrategy> handlerOptions;

    QueryProcessor(Map<Class, QueryHandlerCreationStrategy> handlerOptions) {
        this.handlerOptions = handlerOptions;
    }

    public <TResult> Optional<TResult> process(Query<TResult> query) {
        QueryHandlerCreationStrategy<Query<TResult>, TResult> strategy = handlerOptions.get(query.getClass());
        if (strategy != null) {
            return Optional.of(strategy.createHandler().handle(query));
        }
        return Optional.empty();
    }

    public static QueryProcessor getInstance() {
        if (instance == null) {
            instance = QueryProcessorFactory.createQueryProcessor();
        }
        return instance;
    }
}
