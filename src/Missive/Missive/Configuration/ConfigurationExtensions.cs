using System;

namespace Missive.Configuration
{
    public static class ConfigurationExtensions
    {
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
    }
}