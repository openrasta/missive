using System;
using Missive.Configuration;
using Should;

namespace Tests.Configuration.converters
{
    public class incorrect_untyped : contexts.configurable
    {
        public incorrect_untyped()
        {
            given_config(()=> new MissiveConfiguration().Codec(typeof (Potato)));
        }

        public void error_is_raised()
        {
            error.ShouldBeType<InvalidOperationException>();
        }
    }
}