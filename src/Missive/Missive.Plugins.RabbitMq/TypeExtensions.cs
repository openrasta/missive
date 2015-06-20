using System;
using System.Collections.Generic;
using System.Linq;

namespace Missive.Plugins.RabbitMq
{
    public static class TypeExtensions
    {
        public static IEnumerable<Type> FindParameterForGeneric(this Type targetType, Type interfaceType)
        {
            return from @if in targetType.GetInterfaces()
                where @if.IsGenericType
                where @if.GetGenericTypeDefinition() == interfaceType
                select @if.GetGenericArguments()[0];
        }
    }
}