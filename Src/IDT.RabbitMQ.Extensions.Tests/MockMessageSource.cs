using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using IDT.RabbitMQ.Extensions.Internals;

namespace IDT.RabbitMQ.Extensions.Tests
{
    public class MockMessageSource : IMessageSource
    {
        private readonly ConcurrentQueue<DeliveredRabbitMessage> _messages;
        public Collection<DeliveredRabbitMessage> Acks = new Collection<DeliveredRabbitMessage>();
        public Collection<DeliveredRabbitMessage> Canceled = new Collection<DeliveredRabbitMessage>();
        public Collection<KeyValuePair<DeliveredRabbitMessage, Exception>> Nacks = new Collection<KeyValuePair<DeliveredRabbitMessage, Exception>>();

        public Action<DeliveredRabbitMessage> OnGetNextMessage { get; set; }

        public MockMessageSource(IEnumerable<DeliveredRabbitMessage> messages)
        {
            OnGetNextMessage = m => { };
            _messages = new ConcurrentQueue<DeliveredRabbitMessage>(messages);
        }

        public bool TryGetNextMessage(out DeliveredRabbitMessage message)
        {
            var result = _messages.TryDequeue(out message);
            OnGetNextMessage(message);
            return result;
        }

        public void Acknowledge(DeliveredRabbitMessage message)
        {
            Acks.Add(message);
        }
        
        public void Failure(DeliveredRabbitMessage message, Exception exception)
        {
            Nacks.Add(new KeyValuePair<DeliveredRabbitMessage, Exception>(message,exception));
        }

        public void Cancel(DeliveredRabbitMessage message)
        {
            Canceled.Add(message);
        }
    }
}