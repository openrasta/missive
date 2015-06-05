using System.Linq;
using Missive.Configuration;
using Should;

namespace Tests.Configuration.converters
{
    public class default_untyped : configuration_context
    {
        public default_untyped()
        {
            given_config(new MissiveConfiguration().Converter(typeof(TricorderConverter)));
        }

        public void converter_type_set()
        {
            configuration.ConfigurationModel.Converters.Single().ShouldEqual(typeof (TricorderConverter));
        }
    }
}