using System;
using Fixie;

namespace Tests
{
    public class CustomConvention : Convention
    {
        public CustomConvention()
        {
            Classes.NameEndsWith(string.Empty);
            Methods.Where(_ => _.IsPrivate || _.DeclaringType.IsAbstract == false);

        }
    }
}