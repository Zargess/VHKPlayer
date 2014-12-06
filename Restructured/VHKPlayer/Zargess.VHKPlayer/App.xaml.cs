using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Zargess.VHKPlayer.Settings;

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
    }
}
