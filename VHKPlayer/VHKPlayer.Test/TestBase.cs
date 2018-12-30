using System;
using Autofac;
using Ploeh.AutoFixture;
using VHKPlayer.Infrastructure.Modules;

namespace VHKPlayer.Test
{
    public class TestBase
    {
        protected Fixture Fixture = TestHelper.CreateFixture();

        protected IContainer CreateContainer(Action<ContainerBuilder> setupCallback = null)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new DefaultWiringModule());

            if (setupCallback != null)
            {
                setupCallback(builder);
            }

            return builder.Build();
        }
    }
}