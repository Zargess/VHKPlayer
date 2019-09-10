package com.vhkplayer.dao.queries.getallfolders;

import com.vhkplayer.dao.DataContainer;
import com.vhkplayer.dao.queries.QueryHandlerCreationStrategy;
import com.vhkplayer.folderstructure.FolderNode;

import java.util.List;

public class GetAllFoldersQueryHandlerCreationStrategy implements QueryHandlerCreationStrategy<GetAllFoldersQuery, List<FolderNode>> {
    private DataContainer container;

    public GetAllFoldersQueryHandlerCreationStrategy(DataContainer container) {
        this.container = container;
    }

    @Override
    public GetAllFoldersQueryHandler createHandler() {
        return new GetAllFoldersQueryHandler(container);
    }
}
