using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.FileManagement;
using Zargess.VHKPlayer.FileManagement.Interfaces;
using Zargess.VHKPlayer.SettingsManager;
using Zargess.VHKPlayer.UtilFunctions;

namespace Zargess.VHKPlayer.GUI.ViewModels {
    public class VHKPlayerFactory : IMainViewModelFactory {
        IFolder _rootFolder;
        private IFolder RootFolder {
            get {
                var p = SettingsManagement.Instance.GetStringSetting("root");
                if (_rootFolder == null || _rootFolder.FullPath != p) _rootFolder = new FolderNode(p);
                return _rootFolder;
            }
        }

        public IContainer CreateMiscContainer() {
            return new SingleItemContainer(CreateFolderFromSetting("miscFolder"));
        }

        public IContainer CreateCardContainer() {
            return new SingleItemContainer(CreateFolderFromSetting("cardsFolder"));
        }

        public IContainer CreatePlayerOut() {
            return new SingleItemContainer(CreateFolderFromSetting("playersOutFolder"));
        }

        public IFolder CreateFolder() {
            return RootFolder;
        }

        public ICompositeContainer CreateMusicContainer() {
            return new CompositeSingleItemContainer(CreateFolderFromSetting("musicFolder"));
        }

        public IContainer CreatePlayerContainer() {
            var path = SettingsManagement.Instance.GetPathSetting("playerFolders", 0);
            var playerFolder = new FolderNode(PathHandler.AbsolutePath(path));
            return new PlayerContainer("Players", playerFolder);
        }

        public IContainer CreatePlayListContainer() {
            return new PlayListContainer();
        }

        private IFolder CreateFolderFromSetting(string key) {
            var path = SettingsManagement.Instance.GetStringSetting(key);
            return new FolderNode(PathHandler.AbsolutePath(path));
        }
    }
}