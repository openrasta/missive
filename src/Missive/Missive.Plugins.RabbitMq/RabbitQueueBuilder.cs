using System;
using Missive.Configuration;

namespace Missive.Plugins.RabbitMq
{
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
}