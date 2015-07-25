using Autofac;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using VHKPlayer.Infrastructure.Modules;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Utility.Settings;
using VHKPlayer.Utility.Settings.Interfaces;

namespace VHKPlayer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IContainer container;
        public static IContainer Container
        {
            get
            {
                if (container == null)
                {
                    var builder = new ContainerBuilder();
                    builder.RegisterModule(new DefaultWiringModule());
                    container = builder.Build();
                }
                return container;
            }
        }
    }
}
