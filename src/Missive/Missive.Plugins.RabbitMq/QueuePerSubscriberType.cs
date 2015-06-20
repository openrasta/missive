using System;
using System.Diagnostics;

namespace Missive.Plugins.RabbitMq
{
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
}