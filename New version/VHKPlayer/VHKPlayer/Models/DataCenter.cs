using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using VHKPlayer.Events;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Models
{
    public class DataCenter : IDataCenter
    {
        public ObservableCollection<Player> Players { get; private set; }
        public ObservableCollection<PlayList> PlayLists { get; private set; }
        public ObservableCollection<FolderNode> Folders { get; private set; }
        public ObservableCollection<PlayableFile> PlayableFiles { get; set; }

        public event DataCenterUpdatedEventHandler CenterUpdated;

        public DataCenter()
        {
            Players = new ObservableCollection<Player>();
            PlayLists = new ObservableCollection<PlayList>();
            Folders = new ObservableCollection<FolderNode>();
            PlayableFiles = new ObservableCollection<PlayableFile>();

            Players.CollectionChanged += DataChanged;
            PlayLists.CollectionChanged += DataChanged;
            Folders.CollectionChanged += DataChanged;
            PlayableFiles.CollectionChanged += DataChanged;
        }

        private void DataChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (CenterUpdated == null) return;
            CenterUpdated.Invoke(this, new DataCenterUpdatedEventArgs());
        }
    }
}