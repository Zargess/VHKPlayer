using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Monitors;
using VHKPlayer.Monitors.Interfaces;
using VHKPlayer.Utility.Settings;
using VHKPlayer.Utility.Settings.Interfaces;

namespace VHKPlayer.Infrastructure.Modules
{
    public class DefaultWiringModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var assemblies = new List<Assembly>();
            assemblies.Add(Assembly.GetExecutingAssembly());

            foreach (var assembly in assemblies)
            {
                builder
                    .RegisterAssemblyTypes(assembly)
                    .AsImplementedInterfaces();
            }
            var config = new GlobalConfigService();
            builder.Register<IDataCenter>(c => new DataCenter()).SingleInstance();
            builder.Register<IGlobalConfigService>(c => config).SingleInstance();
            builder.Register<IApplicationMonitor>(c => new ApplicationMonitor(config)).SingleInstance();
        }
    }
}
