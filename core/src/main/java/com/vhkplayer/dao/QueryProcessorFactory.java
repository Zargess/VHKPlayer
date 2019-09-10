package com.vhkplayer.dao;

import com.vhkplayer.dao.queries.QueryHandlerCreationStrategy;
import com.vhkplayer.dao.queries.QueryProcessor;
import com.vhkplayer.dao.queries.getallfolders.GetAllFoldersQuery;
import com.vhkplayer.dao.queries.getallfolders.GetAllFoldersQueryHandlerCreationStrategy;

import java.util.HashMap;
import java.util.Map;

public class QueryProcessorFactory {
    static QueryProcessor createQueryProcessor(DataContainer container) {
        Map<Class, QueryHandlerCreationStrategy> handlerOptions = new HashMap<>();

        handlerOptions.put(GetAllFoldersQuery.class, new GetAllFoldersQueryHandlerCreationStrategy(container));

        return new QueryProcessor(handlerOptions);
    }
}
