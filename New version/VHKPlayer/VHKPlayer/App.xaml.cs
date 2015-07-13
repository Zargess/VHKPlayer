using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using VHKPlayer.Utility.Settings;
using VHKPlayer.Utility.Settings.Interfaces;

namespace VHKPlayer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IGlobalConfigService Config = new GlobalConfigService();
    }
}
