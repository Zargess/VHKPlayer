using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using VHKPlayer.Interfaces;
using VHKPlayer.ViewModels;

namespace VHKPlayer {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        private static IViewModel _viewmodel;
        public static IViewModel ViewModel {
            get {
                if (_viewmodel == null) _viewmodel = new ViewModel();
                return _viewmodel;
            }
        }
    }
}
