using System.Linq;
using System.Threading.Tasks;
using Missive;
using Missive.Configuration;
using Missive.Plugins.RabbitMq;
using Should;
using Tests.Configuration;

namespace Tests.Missive.Plugins.RabbitMq.conventions.pubsub
{
    public class one_publisher_one_subscriber : contexts.configurable
    {
        public one_publisher_one_subscriber()
        {
            given_config( new MissiveConfiguration()
                .Application("test")
                .OnPrepare(_=>new RabbitQueueBuilder().Build(_))
                .Messages().OfType<Fruit>()
                .Handler(()=>new FruitSubscriber()));

            when_preparing_config();
        }

        public void has_one_exchange_named_after_application()
        {
            configuration.ConfigurationModel.Rabbit().Exchanges
                .Single()
                .ShouldBeType<TopicExchange>()
                .Name.ShouldEqual("test");
        }

        public void has_one_queue_named_after_subscriber()
        {
            configuration.ConfigurationModel.Rabbit().Queues.Single()
                .Name.ShouldEqual(new QueuePerSubscriberType("test", typeof(Fruit),typeof (FruitSubscriber)).ToString());
        }

        public void subscriber_queue_is_transient()
        {
            configuration.ConfigurationModel.Rabbit().Queues.Single()
                .Persistent.ShouldBeFalse();
        }
    }

    public class FruitSubscriber : ISubscriber<Fruit>
    {
        public Task Handle(Fruit message)
        {
            return Task.FromResult(0);
        }
    }
}