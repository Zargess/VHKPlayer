using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Enums;
using VHKPlayer.Interfaces;
using VHKPlayer.Utility;

namespace VHKPlayer.Models {
    public class Player : IPlayer, IFolderObserver {
        private List<IPlayerObserver> _observers;
        private ILoadingStrategy<IFile> _loadingStrategy;
        private IFileSelectionStrategy _selectionStrategy;

        public string Name { get; private set; }
        public int Number { get; private set; }
        public bool Trainer { get; private set; }
        public bool Repeat { get; private set; }
        public IStatistics Stats { get; private set; }
        public List<IFile> Content { get; private set; }

        public Player(IFile file, ILoadingStrategy<IFile> loadingStrategy, IFileSelectionStrategy selectionStrategy) {
            Name = file.NameWithoutExtension.Remove(0, 6);
            Number = GeneralFunctions.StringToInteger(file.Name.Substring(0, 3));
            Trainer = Number >= 90;
            Repeat = false;
            _observers = new List<IPlayerObserver>();
            _loadingStrategy = loadingStrategy;
            _selectionStrategy = selectionStrategy;
            Content = LoadFiles(file);
        }

        private List<IFile> LoadFiles(IFile file) {
            return _loadingStrategy.Load(this);
        }

        public Queue<IFile> Play(PlayType type) {
            return _selectionStrategy.SelectFiles(this, type);
        }

        public void AddObserver(IPlayerObserver observer) {
            _observers.Add(observer);
        }

        public void RemoveObserver(IPlayerObserver observer) {
            _observers.Remove(observer);
        }

        public void FolderChanged(IFolder folder) {
            throw new NotImplementedException();
        }
    }
}