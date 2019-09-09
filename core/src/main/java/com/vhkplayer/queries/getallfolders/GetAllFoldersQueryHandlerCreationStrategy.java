package com.vhkplayer.queries.getallfolders;

import com.vhkplayer.folderstructure.FolderNode;
import com.vhkplayer.queries.QueryHandlerCreationStrategy;
import com.vhkplayer.storage.DataContainer;

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
