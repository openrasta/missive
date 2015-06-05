using System.Linq;
using Missive.Configuration;
using Should;

namespace Tests.Configuration.converters
{
    public class default_generic : configuration_context
    {
        public default_generic()
        {
            given_config(new MissiveConfiguration()
                .Converter<TricorderConverter>());
        }

        public void converter_type_set()
        {
            configuration.ConfigurationModel.Converters.Single().ShouldEqual(typeof(TricorderConverter));
        }
    }
}