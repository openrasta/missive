using System.Collections.Generic;

namespace Missive.Plugins.RabbitMq
{
    public class TopicExchange : Exchange
    {
        public TopicExchange(string exchangeName)
        {
            Name = exchangeName;
            Bindings = new List<TopicBinding>();
        }

        public ICollection<TopicBinding> Bindings { get; private set; }
        public Queue BindQueue(string queueName, params string[] routingKeys)
        {
            var queue = new Queue(queueName);
            foreach (var key in routingKeys)
                Bindings.Add(new TopicBinding(key, queue));
            return queue;
        }
    }
}