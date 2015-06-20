using System;

namespace Missive.Configuration
{
    public static class ConverterExtensions
    {
        public static IMissiveConfiguration Codec<T>(this IMissiveConfiguration configuration)
            where T:ICodec
        {
            configuration.ConfigurationModel.Converters.Add(typeof (T));
            return configuration;
        }

        public static IMissiveConfiguration Codec(this IMissiveConfiguration configuration, Type converterType)
        {
            if (!typeof(ICodec).IsAssignableFrom(converterType))
                throw new InvalidOperationException(string.Format("Type '{0}' does not implement '{1}'.", converterType.Name, typeof(ICodec).Name));
            configuration.ConfigurationModel.Converters.Add(converterType);

            return configuration;
        }
    }
}