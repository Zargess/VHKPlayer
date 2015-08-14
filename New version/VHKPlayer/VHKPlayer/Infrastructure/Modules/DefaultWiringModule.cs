using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.DataManagement;
using VHKPlayer.DataManagement.Interfaces;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;

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

            builder.Register<IDataCenter>(c => new DataCenter()).SingleInstance();
        }
    }
}
