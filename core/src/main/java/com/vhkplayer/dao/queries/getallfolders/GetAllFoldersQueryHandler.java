package com.vhkplayer.dao.queries.getallfolders;

import com.vhkplayer.dao.DataContainer;
import com.vhkplayer.dao.queries.QueryHandler;
import com.vhkplayer.folderstructure.FolderNode;

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
