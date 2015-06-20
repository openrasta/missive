using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Missive;
using Missive.Configuration;
using Missive.Converters.JsonNet;
using Should;
using Tests.Configuration;

namespace Tests.Missive.PubSub
{
    public class one_subscriber : contexts.configurable
    {
        public one_subscriber()
        {
            given_missive(
                new MissiveConfiguration()
                    .Codec<JsonNetCodec>()
                    .Messages().OfType<Fruit>()
                    .Handler(()=> new Blender()));
            when_publishing(new Fruit());
        }

        public void message_is_received()
        {
            //Blender.Fruits.Single().ShouldBeType<Fruit>();
        }
    }
   
    public class Blender : ISubscriber<Fruit>
    {
        public Blender()
        {
            Fruits = new ConcurrentBag<Fruit>();
        }
        public static ConcurrentBag<Fruit> Fruits { get; private set; }
        public Task Handle(Fruit message)
        {
            Fruits.Add(message);
            return Task.FromResult(0);
        }
    }
}