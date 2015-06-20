using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Missive;
using Missive.Configuration;

namespace Missive.Plugins.RabbitMq
{

    public class Bus
    {
        public TopicExchange ToppicExchange(string name)
        {
            return new TopicExchange(name);
        }
    }
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

    public class Exchange
    {
        public string Name { get; set; }
    }

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
    public class Queue
    {
        public string Name { get; set; }
        public bool Persistent { get; set; }

        public Queue(string name)
        {
            Name = name;
        }
    }

    public class QueuePerSubscriberType
    {
        private readonly string _applicationName;
        private readonly Type _messageType;
        private readonly Type _subscriberType;

        public QueuePerSubscriberType(string applicationName, Type messageType, Type subscriberType)
        {
            _applicationName = applicationName;
            _messageType = messageType;
            _subscriberType = subscriberType;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}:{2}:{3}:{4}", _applicationName, _messageType.Name, Environment.MachineName,
                _subscriberType.Name, Process.GetCurrentProcess().Id);
        }
    }
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
    public class RabbitQueueBuilder
    {
        public void Build(ConfigurationModel configurationModel)
        {
            var model = configurationModel.Rabbit();
            var appExchange = new TopicExchange(configurationModel.Application);
            model.Exchanges.Add(appExchange);
            foreach (var subscriber in configurationModel.Handlers)
                foreach (var messageType in subscriber.Type.FindParameterForGeneric(typeof(ISubscriber<>)))
                    appExchange.BindQueue(GetQueueName(configurationModel, messageType, subscriber), messageType.FullName);
        }

        private string GetQueueName(ConfigurationModel configurationModel, Type messageType, HandlerModel subscriber)
        {
            return new QueuePerSubscriberType(
                configurationModel.Application,
                messageType,
                subscriber.Type
                ).ToString();
        }
    }

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
    public static class RabbitConfigurationExtensions
    {
        public static RabbitModel Rabbit(this ConfigurationModel model)
        {
            object rabbitModel;
            if (!model.Extensions.TryGetValue("rabbit", out rabbitModel))
                rabbitModel = model.Extensions["rabbit"] = new RabbitModel();
            return (RabbitModel)rabbitModel;
        }
    }
    public class ConcurrentQueueConsumer
    {
        private readonly Func<RawMessage, Task> _handler;
        private readonly Func<RawMessage, Task> _onSuccess;
        private readonly Func<RawMessage, Exception, Task> _onFail;
        ConcurrentQueue<RawMessage> messages = new ConcurrentQueue<RawMessage>();

        public ConcurrentQueueConsumer(Func<RawMessage, Task> handler, Func<RawMessage, Task> onSuccess, Func<RawMessage, Exception, Task> onFail)
        {
            _handler = handler;
            _onSuccess = onSuccess;
            _onFail = onFail;
        }

        void Enqueue(RawMessage message)
        {
            messages.Enqueue(message);
            Task.Factory.StartNew(() => ReadMessage(), TaskCreationOptions.DenyChildAttach | TaskCreationOptions.HideScheduler);
        }

        private async Task ReadMessage()
        {
            RawMessage message;
            if (messages.TryDequeue(out message) == false)
                return;
            Exception error = null;
            try
            {
                await _handler(message);
            }
            catch (Exception e)
            {
                error = e;
            }
            if (error == null)
                await _onSuccess(message);
            else
                await _onFail(message, error);
        }
    }
}
