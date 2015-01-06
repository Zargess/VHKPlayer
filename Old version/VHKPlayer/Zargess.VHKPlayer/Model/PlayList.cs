﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Zargess.VHKPlayer.Collections;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.Model {
    public class PlayList : IPlayList {
        private ILoadingStrategy<IFile> LoadingStrategy { get; set; }
        public string Name { get; private set; }
        public ObservableCollection<IFile> Content { get; private set; }
        public bool Repeat { get; set; }
        private IFolder Folder { get; set; }
        public bool HasAudio { get; private set; }

        public Dispatcher Disp { get; private set; }

        public IFileSelectionStrategy SelectionStrategy { get; private set; }

        public PlayList(IPlayListFactory factory) {
            Content = new SortableCollection<IFile>();
            Name = factory.CreateName();
            SelectionStrategy = factory.CreateSelectionStrategy();
            Folder = factory.CreateFolder();
            LoadingStrategy = factory.CreateLoadingStrategy();
            Repeat = factory.CreateRepeat();
            HasAudio = factory.CreateHasAudio();
            LoadingStrategy.Load(Content);
            Disp = Dispatcher.CurrentDispatcher;
            Folder.InitWatcher();
            Folder.FolderChanged += FolderChanged;
        }

        private void FolderChanged(object sender, EventArgs e) {
            Console.WriteLine(Name);
            Action action = () => {
                LoadingStrategy.Load(Content);
            };
            Disp.BeginInvoke(action);
        }

        public void Add(IFile file) {
            Content.Add(file);
        }

        public Queue<IFile> Play(PlayType pt) {
            return SelectionStrategy.SelectFiles(this, pt);
        }

        public override string ToString() {
            return Name;
        }
    }
}