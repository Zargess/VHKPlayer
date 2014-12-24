using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Zargess.VHKPlayer.Factories.IPlayManagers;
using Zargess.VHKPlayer.Factories.ViewModels;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.PlayManaging;
using Zargess.VHKPlayer.Settings;
using Zargess.VHKPlayer.Strategies.Playing;
using Zargess.VHKPlayer.ViewModels;

namespace Zargess.VHKPlayer {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        private static GlobalConfigService _configService;
        public static GlobalConfigService ConfigService {
            get {
                if (_configService == null) _configService = new GlobalConfigService(new VHKSettings());
                return _configService;
            }
        }

        private static GlobalConfigService _guiconfigservice;
        public static GlobalConfigService GuiConfigService {
            get {
                if (_guiconfigservice == null) _guiconfigservice = new GlobalConfigService(new GUISettings());
                return _guiconfigservice;
            }
        }

        private static VideoPlayerViewModel _videoplayerviewmodel;
        public static VideoPlayerViewModel PlayerViewModel {
            get {
                if (_videoplayerviewmodel == null) _videoplayerviewmodel = new VideoPlayerViewModel(new VideoPlayerFactory());
                return _videoplayerviewmodel;
            }
        }

        private static IPlayManager _playManager;
        public static IPlayManager PlayManager {
            get {
                if (_playManager == null) _playManager = new PlayManager(new PlayManagerFactory());
                return _playManager;
            }
        }
    }
}
