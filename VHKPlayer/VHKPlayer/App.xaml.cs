using System.Windows;
using System.Windows.Threading;
using Autofac;
using VHKPlayer.Infrastructure.Modules;

namespace VHKPlayer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IContainer _container;

        public static IContainer Container
        {
            get
            {
                if (_container == null)
                {
                    var builder = new ContainerBuilder();
                    builder.RegisterModule(new DefaultWiringModule());
                    _container = builder.Build();
                }

                return _container;
            }
        }

        public static Dispatcher Dispatch { get; set; }
    }
}