using System.Text;
using Missive;

namespace Missive.Plugins.RabbitMq
{

    public class Bus
    {
        public TopicExchange ToppicExchange(string name)
        {
            return new TopicExchange(name);
        }
    }
}
