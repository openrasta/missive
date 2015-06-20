using System.Collections.Generic;
using System.Linq;

namespace Missive.Plugins.RabbitMq
{
    public class RabbitModel
    {
        public RabbitModel()
        {
            Exchanges = new List<Exchange>();
        }

        public IEnumerable<Queue> Queues
        {
            get
            {
                return (from exchange in Exchanges.OfType<TopicExchange>()
                    from binding in exchange.Bindings
                    select binding.Queue).Distinct();
            }
        }

        public ICollection<Exchange> Exchanges { get; private set; }
    }
}