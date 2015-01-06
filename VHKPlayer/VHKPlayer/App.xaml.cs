using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using VHKPlayer.Facade;
using VHKPlayer.Interfaces;

namespace VHKPlayer {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        private static IVHKPlayer _vhkPlayer;
        public static IVHKPlayer VHKPlayer {
            get {
                if (_vhkPlayer == null) _vhkPlayer = new VHKPlayerImpl();
                return _vhkPlayer;
            }
        }
    }
}
