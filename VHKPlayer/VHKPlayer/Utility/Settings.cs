using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Threading;
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
                _playerPictureFolder = GenerateFolderFromSetting("playerFolderPicture", _playerPictureFolder);
                return _playerPictureFolder;
            }
        }

        private static IFolder _playerVideoFolder;
        public static IFolder PlayerVideoFolder {
            get {
                _playerVideoFolder = GenerateFolderFromSetting("playerFolderVideo", _playerVideoFolder);
                return _playerVideoFolder;
            }
        }

        private static IFolder _playerStatPictureFolder;
        public static IFolder PlayerStatPictureFolder {
            get {
                _playerStatPictureFolder = GenerateFolderFromSetting("playerFolderStatPicture", _playerStatPictureFolder);
                return _playerStatPictureFolder;
            }
        }

        private static IFolder _playerStatVideoFolder;
        public static IFolder PlayerStatVideoFolder {
            get {
                _playerStatVideoFolder = GenerateFolderFromSetting("playerFolderStatVideo", _playerStatVideoFolder);
                return _playerStatVideoFolder;
            }
        }

        private static IFolder _playerStatMusicFolder;
        public static IFolder PlayerStatMusicFolder {
            get {
                _playerStatMusicFolder = GenerateFolderFromSetting("playerFolderStatMusic", _playerStatMusicFolder);
                return _playerStatMusicFolder;
            }
        }

        private static IFolder _statFolder;
        public static IFolder StatFolder {
            get {
                _statFolder = GenerateFolderFromSetting("statFolder", _statFolder);
                return _statFolder;
            }
        }

        private static List<Timer> _timers;
        public static List<Timer> Timers {
            get {
                if (_timers == null) _timers = new List<Timer>();
                return _timers;
            }
        }

        public static DispatcherTimer PlayerStatTimer { get; set; }

        public static void CancelStatTimer() {
            if (PlayerStatTimer == null) return;
            PlayerStatTimer.Stop();
        }

        private static IFolder GenerateFolderFromSetting(string setting, IFolder folder) {
            var path = GeneralFunctions.AbsolutePath(FolderConfig.GetString(setting));
            if (folder == null) return new FolderNode(path);
            if (folder.FullPath != path) return new FolderNode(path);
            return folder;
        }

        public static List<string> RequiredFolders {
            get {
                return FolderConfig.GetString("requiredFolders").Split(';').ToList();
            }
        }

        public static List<string> SupportedMusic {
            get {
                return FolderConfig.GetString("supportedMusic").Split(';').ToList();
            }
        }

        public static List<string> SupportedPicture {
            get {
                return FolderConfig.GetString("supportedPicture").Split(';').ToList();
            }
        }

        public static List<string> SupportedVideo {
            get {
                return FolderConfig.GetString("supportedVideo").Split(';').ToList();
            }
        }

        public static List<string> SupportedInfo {
            get {
                return FolderConfig.GetString("supportedInfo").Split(';').ToList();
            }
        }
    }
}