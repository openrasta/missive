using System.Linq;
using Missive.Configuration;
using Should;
using Tests.Missive.PubSub;

namespace Tests.Configuration
{
    public class metamodel_properties : contexts.configurable
    {
        public metamodel_properties()
        {
            given_config(new MissiveConfiguration()
                .Application("myApplication")
                .Messages()
                .OfType<Fruit>()
                .OfType<Potato>().Only()
                .Handler(()=> new Blender())
                .Provider("inmem:///"));
        }

        public void provider_correct()
        {
            configuration.ConfigurationModel.Provider.ShouldEqual("inmem:///");
        }

        public void message_base_is_correct()
        {
            configuration.ConfigurationModel.MessageFamilies.First(_ => _.Type == typeof (Fruit)).Covariant.ShouldBeTrue();
        }

        public void message_exact_type_is_correct()
        {
            configuration.ConfigurationModel.MessageFamilies.First(_ => _.Type == typeof(Potato)).Covariant.ShouldBeFalse();

        }
        public void application_name_correct()
        {
            configuration.ConfigurationModel.Application.ShouldEqual("myApplication");
        }
    }
}