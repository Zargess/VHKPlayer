using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Interfaces;
using VHKPlayer.Models;

namespace VHKPlayer.Utility {
    public class Settings {
        private static IGlobalConfigService _folderConfig;
        public static IGlobalConfigService FolderConfig {
            get {
                return _folderConfig;
            }
            set {
                _folderConfig = value;
            }
        }

        private static IFolder _playerPictureFolder;
        public static IFolder PlayerPictureFolder {
            get {
                var path = GeneralFunctions.AbsolutePath(FolderConfig.GetString("playerFolderPicture"));
                if (_playerPictureFolder == null) _playerPictureFolder = new FolderNode(path);
                if (_playerPictureFolder.FullPath != path) _playerPictureFolder = new FolderNode(path);
                return _playerPictureFolder;
            }
        }

        private static IFolder _playerVideoFolder;
        public static IFolder PlayerVideoFolder {
            get {
                var path = GeneralFunctions.AbsolutePath(FolderConfig.GetString("playerFolderVideo"));
                if (_playerVideoFolder == null) _playerVideoFolder = new FolderNode(path);
                if (_playerVideoFolder.FullPath != path) _playerVideoFolder = new FolderNode(path);
                return _playerVideoFolder;
            }
        }

        private static IFolder _playerStatFolder;
        public static IFolder PlayerStatFolder {
            get {
                var path = GeneralFunctions.AbsolutePath(FolderConfig.GetString("playerFolderStat"));
                if (_playerStatFolder == null) _playerStatFolder = new FolderNode(path);
                if (_playerStatFolder.FullPath != path) _playerStatFolder = new FolderNode(path);
                return _playerStatFolder;
            }
        }
    }
}
