using System;
using Autofac;

namespace Tests
{
    public class Class2
    {
        public void TestStuff()
        {
            var af = new ContainerBuilder();
            af.RegisterModule<Module1>();
            af.RegisterModule<Module2>();
            af.Build();
        } 

    }

    public class Module2 : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(_ => new Stuff2())
                .OnActivating(_ => Console.WriteLine(2))
                .AutoActivate();
            base.Load(builder);
        }
    }
    public class Module1 : Module
    {
        
        protected override void Load(ContainerBuilder builder)
        {

            builder.Register(_ => new Stuff())
                .OnActivating(_ => Console.WriteLine(1))
                .AutoActivate();
        }
    }
    public class Stuff2 { }
    public class Stuff { }
}