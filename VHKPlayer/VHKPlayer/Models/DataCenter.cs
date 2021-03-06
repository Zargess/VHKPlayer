﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using VHKPlayer.Infrastructure;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Models
{
    public class DataCenter : IDataCenter
    {
        private readonly List<IDataObserver> _observers;

        public ObservableCollection<Player> Players { get; private set; }
        public ObservableCollection<PlayList> PlayLists { get; private set; }
        public ObservableCollection<FolderNode> Folders { get; private set; }
        public ObservableCollection<PlayableFile> PlayableFiles { get; private set; }

        public bool UncommitedChanges { get; set; }

        public DataCenter()
        {
            Players = new ObservableCollection<Player>();
            PlayLists = new ObservableCollection<PlayList>();
            Folders = new ObservableCollection<FolderNode>();
            PlayableFiles = new ObservableCollection<PlayableFile>();
            _observers = new List<IDataObserver>();
            Players.CollectionChanged += DataChanged;
            PlayLists.CollectionChanged += DataChanged;
            Folders.CollectionChanged += DataChanged;
            PlayableFiles.CollectionChanged += DataChanged;
        }

        private void DataChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UncommitedChanges = true;
        }

        public void AddObserver(IDataObserver observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IDataObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Commit()
        {
            PlayableFiles.SetCollection(PlayableFiles.OrderBy(x => x.Name));
            Players.SetCollection(Players.OrderBy(x => x.Number));
            PlayLists.SetCollection(PlayLists.OrderBy(x => x.Name));
            foreach (var observer in _observers)
            {
                observer.DataUpdated();
            }

            UncommitedChanges = false;
        }
    }
}