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
            throw new NotImplementedException();
        }

        public IContainer CreateCardContainer() {
            throw new NotImplementedException();
        }

        public IContainer CreatePlayerOut() {
            throw new NotImplementedException();
        }

        public IFolder CreateFolder() {
            return RootFolder;
        }

        public ICompositeContainer CreateMusicContainer() {
            var path = SettingsManagement.Instance.GetStringSetting("musicFolder");
            var musicFolder = new FolderNode(PathHandler.AbsolutePath(path));
            return new CompositeSingleItemContainer(musicFolder);
        }

        public IContainer CreatePlayerContainer() {
            var path = SettingsManagement.Instance.GetPathSetting("playerFolders", 0);
            var playerFolder = new FolderNode(PathHandler.AbsolutePath(path));
            return new PlayerContainer("Players", playerFolder);
        }

        public IContainer CreatePlayListContainer() {
            return new PlayListContainer();
        }
    }
}