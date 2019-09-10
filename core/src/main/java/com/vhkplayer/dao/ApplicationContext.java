package com.vhkplayer.dao;

import com.vhkplayer.dao.commands.CommandProcessor;
import com.vhkplayer.dao.queries.QueryProcessor;

public final class ApplicationContext {
    private static DataContainer dataContainer;

    public CommandProcessor getCommandProcessor() {
        return CommandProcessorFactory.createCommandProcessor(ApplicationContext.getDataContainerInstance());
    }

    public QueryProcessor getQueryProcessor() {
        return QueryProcessorFactory.createQueryProcessor(ApplicationContext.getDataContainerInstance());
    }

    private static DataContainer getDataContainerInstance() {
        if (dataContainer == null) {
            dataContainer = new DataContainer();
        }
        return dataContainer;
    }
}
