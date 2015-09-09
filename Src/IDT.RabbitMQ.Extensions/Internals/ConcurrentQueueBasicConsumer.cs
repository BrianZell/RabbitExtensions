using System;
using System.Collections.Concurrent;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace IDT.RabbitMQ.Extensions.Internals
{
    public class ConcurrentQueueBasicConsumer : DefaultBasicConsumer
    {
        private readonly ConcurrentQueue<DeliveredRabbitMessage> _queue;

        public ConcurrentQueueBasicConsumer(ConcurrentQueue<DeliveredRabbitMessage> queue)
        {
            _queue = queue;
        }

        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, byte[] body)
        {
            _queue.Enqueue(new DeliveredRabbitMessage
                               {
                                   DeliveryTag = deliveryTag,
                                   Redelivered = redelivered,
                                   Exchange = exchange,
                                   RoutingKey = routingKey,
                                   Properties = properties,
                                   Body = body,
                               });
        }
    }
}
