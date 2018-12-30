using System.Collections.Generic;
using System.ComponentModel;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Monitors.Interfaces;
using VHKPlayer.Utility.Settings.Interfaces;

namespace VHKPlayer.Monitors
{
    public class SettingsMonitor : IApplicationMonitor, ISettingsMonitor
    {
        private readonly List<IApplicationObserver> _appObservers;
        private readonly List<ISettingsObserver> _settingsObservers;

        public SettingsMonitor(IGlobalConfigService config)
        {
            _appObservers = new List<IApplicationObserver>();
            _settingsObservers = new List<ISettingsObserver>();
            config.ApplicationSettingsUpdated += ApplicationChanged;
            config.SettingsUpdated += SettingsChanged;
        }

        private void SettingsChanged(object sender, PropertyChangedEventArgs e)
        {
            _settingsObservers.ForEach(x => x.SettingsChanged(e.PropertyName));
        }

        private void ApplicationChanged(object sender, PropertyChangedEventArgs e)
        {
            _appObservers.ForEach(x => x.ApplicationChanged(e.PropertyName));
        }

        public void AddObserver(IApplicationObserver observer)
        {
            _appObservers.Add(observer);
        }

        public void RemoveObserver(IApplicationObserver observer)
        {
            _appObservers.Remove(observer);
        }

        public void AddObserver(ISettingsObserver observer)
        {
            _settingsObservers.Add(observer);
        }

        public void RemoveObserver(ISettingsObserver observer)
        {
            _settingsObservers.Remove(observer);
        }
    }
}