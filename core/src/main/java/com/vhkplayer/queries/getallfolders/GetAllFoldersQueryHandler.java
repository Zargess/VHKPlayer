package com.vhkplayer.queries.getallfolders;

import com.vhkplayer.folderstructure.FolderNode;
import com.vhkplayer.queries.QueryHandler;
import com.vhkplayer.storage.DataContainer;

import java.util.List;

public class GetAllFoldersQueryHandler implements QueryHandler<GetAllFoldersQuery, List<FolderNode>> {
    private DataContainer container;

    public GetAllFoldersQueryHandler(DataContainer container) {
        this.container = container;
    }

    @Override
    public List<FolderNode> handle(GetAllFoldersQuery query) {
        return container.getFolders();
    }
}
