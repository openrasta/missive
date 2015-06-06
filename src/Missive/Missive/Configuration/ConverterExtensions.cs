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
            if (!typeof(IConvertMessages).IsAssignableFrom(converterType))
                throw new InvalidOperationException(string.Format("Type '{0}' does not implement '{1}'.", converterType.Name, typeof(IConvertMessages).Name));
            configuration.ConfigurationModel.Converters.Add(converterType);

            return configuration;
        }
    }
}