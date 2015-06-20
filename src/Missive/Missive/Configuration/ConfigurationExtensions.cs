using System;

namespace Missive.Configuration
{
    public static class ConfigurationExtensions
    {
        public static IMissiveConfiguration OnPrepare(this IMissiveConfiguration configuration,
            Action<ConfigurationModel> action)
        {
            configuration.ConfigurationModel.Prepare += (sender, model) => action(model);
            return configuration;
        }
        public static IMissiveConfiguration Provider(this IMissiveConfiguration configuration, string providerUri)
        {
            configuration.ConfigurationModel.Provider = providerUri;
            return configuration;
        }
        public static IMessageSection Messages(this IMissiveConfiguration configuration)
        {
            return new MessageConfig(configuration);
        }
        public static IMissiveConfiguration Application(this IMissiveConfiguration configuration, string name)
        {
            if (name == null) throw new ArgumentNullException("name");
            configuration.ConfigurationModel.Application = name;
            return configuration;
        }

        public static IMissiveConfiguration Handler<T>(this IMissiveConfiguration configuration, Func<T> handler)
        {
            configuration.ConfigurationModel.Handlers.Add(new HandlerModel
            {
                Type = typeof (T),
                Factory = ()=>handler()
            });
            return configuration;
        }
    }

    public class HandlerModel
    {
        public Type Type { get; set; }
        public Func<object> Factory { get; set; }
    }
}