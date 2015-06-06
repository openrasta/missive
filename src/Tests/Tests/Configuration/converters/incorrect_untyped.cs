using System;
using Missive.Configuration;
using Should;

namespace Tests.Configuration.converters
{
    public class incorrect_untyped : configuration_context
    {
        public incorrect_untyped()
        {
            given_config(()=> new MissiveConfiguration().Converter(typeof (Potato)));
        }

        public void error_is_raised()
        {
            error.ShouldBeType<InvalidOperationException>();
        }
    }
}