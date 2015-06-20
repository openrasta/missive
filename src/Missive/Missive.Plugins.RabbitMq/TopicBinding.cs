namespace Missive.Plugins.RabbitMq
{
    public class TopicBinding
    {
        public TopicBinding(string routingKey, Queue queue)
        {
            RoutingKey = routingKey;
            Queue = queue;
        }

        public string RoutingKey { get; set; }
        public Queue Queue { get; set; }
    }
}