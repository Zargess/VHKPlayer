using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHKPlayer.Test
{
    public static class TestHelper
    {
        public static Fixture CreateFixture()
        {
            var fixture = new Fixture();
            fixture.OmitAutoProperties = true;
            fixture.RepeatCount = 2;

            //we remove throwing behaviors on circular structures, since this is based on an ORM.
            var behavior = fixture
                .Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .Single();
            fixture.Behaviors.Remove(behavior);

            //we want circular class definitions because this is based on an ORM.
            fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));

            return fixture;
        }
    }
}
