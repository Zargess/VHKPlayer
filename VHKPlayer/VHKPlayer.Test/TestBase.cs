using Autofac;
using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Infrastructure.Modules;

namespace VHKPlayer.Test
{
    public class TestBase
    {
        protected Fixture fixture = TestHelper.CreateFixture();

        protected IContainer CreateContainer(Action<ContainerBuilder> setupCallback = null)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new DefaultWiringModule());

            if(setupCallback != null)
            {
                setupCallback(builder);
            }

            return builder.Build();
        }
    }
}
