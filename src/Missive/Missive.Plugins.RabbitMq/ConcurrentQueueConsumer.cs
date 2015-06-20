using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Missive.Plugins.RabbitMq
{
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