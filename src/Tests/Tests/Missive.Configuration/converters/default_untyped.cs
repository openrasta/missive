using System.Linq;
using Missive.Configuration;
using Should;

namespace Tests.Configuration.converters
{
    public class default_untyped : contexts.configurable
    {
        public default_untyped()
        {
            given_config(new MissiveConfiguration().Codec(typeof(TricorderConverter)));
        }

        public void converter_type_set()
        {
            configuration.ConfigurationModel.Converters.Single().ShouldEqual(typeof (TricorderConverter));
        }
    }
}