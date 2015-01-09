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
        public string Name { get; private set; }
        public int Number { get; private set; }
        public bool Trainer { get; private set; }
        public bool Repeat { get; private set; }
        public IStatistics Stats { get; private set; }
        public List<IFile> Content { get; private set; }

        public Player(IFile file) {
            Name = file.NameWithoutExtension.Remove(0, 6);
            Number = GeneralFunctions.StringToInteger(file.Name.Substring(0, 3));
            Trainer = Number >= 90;
            Repeat = false;
            _observers = new List<IPlayerObserver>();
            Content = LoadFiles(file);
        }

        // TODO : Move to strategy
        private List<IFile> LoadFiles(IFile file) {
            var res = new List<IFile>();
            var folders = GeneralFunctions.GetPlayerFolderPaths().Select(x => new FolderNode(x));

            foreach (var folder in folders) {
                var f = folder.Content.SingleOrDefault(x => x.NameWithoutExtension.ToLower() == file.NameWithoutExtension.ToLower());
                if (f == null) continue;
                res.Add(f);
            }

            // TODO : Check if player is missing any files and create notification if he does

            return res;
        }

        public Queue<IFile> Play(PlayType type) {
            throw new NotImplementedException();
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