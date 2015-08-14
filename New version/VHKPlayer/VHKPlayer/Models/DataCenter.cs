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
        private List<IDataObserver> observers;
        public ObservableCollection<Player> Players { get; private set; }
        public ObservableCollection<PlayList> PlayLists { get; private set; }
        public ObservableCollection<FolderNode> Folders { get; private set; }
        public ObservableCollection<PlayableFile> PlayableFiles { get; set; }

        public DataCenter()
        {
            Players = new ObservableCollection<Player>();
            PlayLists = new ObservableCollection<PlayList>();
            Folders = new ObservableCollection<FolderNode>();
            PlayableFiles = new ObservableCollection<PlayableFile>();
            observers = new List<IDataObserver>();
            Players.CollectionChanged += DataChanged;
            PlayLists.CollectionChanged += DataChanged;
            Folders.CollectionChanged += DataChanged;
            PlayableFiles.CollectionChanged += DataChanged;
        }

        private void DataChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var type = PlayableType.PlayableFile;

            if (sender == Players) type = PlayableType.Player;
            else if (sender == PlayLists) type = PlayableType.PlayList;
            else if (sender == PlayableFiles) type = PlayableType.PlayableFile;
            else return;

            foreach (var observer in observers)
            {
                observer.DataUpdated(type);
            }
        }

        public void AddObserver(IDataObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IDataObserver observer)
        {
            observers.Remove(observer);
        }
    }
}