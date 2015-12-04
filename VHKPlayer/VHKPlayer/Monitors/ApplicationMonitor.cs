using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Monitors.Interfaces;
using VHKPlayer.Utility.Settings.Interfaces;

namespace VHKPlayer.Monitors
{
    public class ApplicationMonitor : IApplicationMonitor
    {
        private readonly List<IApplicationObserver> _observers;

        public ApplicationMonitor(IGlobalConfigService config)
        {
            _observers = new List<IApplicationObserver>();
            config.ApplicationSettingsUpdated += ApplicationChanged;
        }

        private void ApplicationChanged(object sender, PropertyChangedEventArgs e)
        {
            _observers.ForEach(x => x.ApplicationChanged(e.PropertyName));
        }

        public void AddObserver(IApplicationObserver observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IApplicationObserver observer)
        {
            _observers.Remove(observer);
        }
    }
}
