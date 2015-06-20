using System;
using Fixie;

namespace Tests
{
    public class CustomConvention : Convention
    {
        public CustomConvention()
        {
            Classes.Where(_ => char.IsLower(_.Name[0]))
                   .NameEndsWith(string.Empty);
            Methods.Where(_ => _.IsPrivate || _.IsPublic || _.DeclaringType.IsAbstract == false);

        }
    }
}