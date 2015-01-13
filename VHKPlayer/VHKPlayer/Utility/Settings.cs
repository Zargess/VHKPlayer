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
                return GenerateFolderFromSetting("playerFolderPicture", _playerPictureFolder);
            }
        }

        private static IFolder _playerVideoFolder;
        public static IFolder PlayerVideoFolder {
            get {
                return GenerateFolderFromSetting("playerFolderVideo", _playerVideoFolder);
            }
        }

        private static IFolder _playerStatPictureFolder;
        public static IFolder PlayerStatPictureFolder {
            get {
                return GenerateFolderFromSetting("playerFolderStatPicture", _playerStatPictureFolder);
            }
        }

        private static IFolder _playerStatVideoFolder;
        public static IFolder PlayerStatVideoFolder {
            get {
                return GenerateFolderFromSetting("playerFolderStatVideo", _playerStatVideoFolder);
            }
        }

        private static IFolder _playerStatMusicFolder;
        public static IFolder PlayerStatMusicFolder {
            get {
                return GenerateFolderFromSetting("playerFolderStatMusic", _playerStatMusicFolder);
            }
        }

        private static IFolder GenerateFolderFromSetting(string setting, IFolder folder) {
            var path = GeneralFunctions.AbsolutePath(FolderConfig.GetString(setting));
            if (folder == null) return new FolderNode(path);
            if (folder.FullPath != path) return new FolderNode(path);
            return folder;
        }
    }
}
