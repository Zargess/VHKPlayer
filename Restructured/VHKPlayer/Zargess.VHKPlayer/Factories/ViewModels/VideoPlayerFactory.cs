using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.Model;
using Zargess.VHKPlayer.Strategies.Loading.IContainers;
using Zargess.VHKPlayer.Utility;

namespace Zargess.VHKPlayer.Factories.ViewModels {
    public class VideoPlayerFactory : IVideoPlayerFactory {
        IFolder _rootFolder;
        private IFolder RootFolder {
            get {
                var p = App.ConfigService.GetString("root");
                if (_rootFolder == null || _rootFolder.FullPath != p) _rootFolder = new FolderNode(p);
                return _rootFolder;
            }
        }

        public IContainer<IPlayable> CreateMiscContainer() {
            return new SingleItemContainer(CreateFolderFromSetting("miscFolder"));
        }

        public IContainer<IPlayable> CreateCardContainer() {
            return new SingleItemContainer(CreateFolderFromSetting("cardsFolder"));
        }

        public IContainer<IPlayable> CreatePlayerOut() {
            return new SingleItemContainer(CreateFolderFromSetting("playersOutFolder"));
        }

        public IFolder CreateFolder() {
            return RootFolder;
        }

        public IContainer<IContainer<IPlayable>> CreateMusicContainer() {
            return new MusicFolderContainer(CreateFolderFromSetting("musicFolder"));
        }

        public IContainer<IPlayable> CreatePlayerContainer() {
            var path = App.ConfigService.GetPathString("playerFolders", 0);
            var playerFolder = new FolderNode(PathHandler.AbsolutePath(path));
            return new PlayerContainer("Players", playerFolder);
        }

        public IContainer<IPlayable> CreatePlayListContainer() {
            return new PlayListContainer(new PlayListContainerLoadingStrategy());
        }

        private IFolder CreateFolderFromSetting(string key) {
            var path = App.ConfigService.GetString(key);
            return new FolderNode(PathHandler.AbsolutePath(path));
        }
    }
}
