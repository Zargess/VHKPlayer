package com.vhkplayer.queries;

import com.vhkplayer.queries.getallfolders.GetAllFoldersQuery;
import com.vhkplayer.queries.getallfolders.GetAllFoldersQueryHandlerCreationStrategy;
import com.vhkplayer.storage.DataContainer;

import java.util.HashMap;
import java.util.Map;

public class QueryProcessorFactory {
    static QueryProcessor createQueryProcessor() {
        DataContainer container = DataContainer.getInstance();

        Map<Class, QueryHandlerCreationStrategy> handlerOptions = new HashMap<>();

        handlerOptions.put(GetAllFoldersQuery.class, new GetAllFoldersQueryHandlerCreationStrategy(container));

        return new QueryProcessor(handlerOptions);
    }
}
