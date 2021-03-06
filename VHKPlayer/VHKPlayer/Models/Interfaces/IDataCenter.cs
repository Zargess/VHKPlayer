﻿using System.Collections.ObjectModel;

namespace VHKPlayer.Models.Interfaces
{
    public interface IDataCenter
    {
        ObservableCollection<Player> Players { get; }
        ObservableCollection<PlayList> PlayLists { get; }
        ObservableCollection<FolderNode> Folders { get; }
        ObservableCollection<PlayableFile> PlayableFiles { get; }
        bool UncommitedChanges { get; set; }

        void AddObserver(IDataObserver observer);
        void RemoveObserver(IDataObserver observer);
        void Commit();
    }
}