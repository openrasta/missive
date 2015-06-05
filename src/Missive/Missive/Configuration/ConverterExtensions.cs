using System;

namespace Missive.Configuration
{
    public static class ConverterExtensions
    {
        public static IMissiveConfiguration Converter<T>(this IMissiveConfiguration configuration)
            where T:IConvertMessages
        {
            configuration.ConfigurationModel.Converters.Add(typeof (T));
            return configuration;
        }

        public static IMissiveConfiguration Converter(this IMissiveConfiguration configuration, Type converterType)
        {
            configuration.ConfigurationModel.Converters.Add(converterType);

            return configuration;
        }
    }
}