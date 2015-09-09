using RabbitMQ.Client;
using System;
using System.Collections.Generic;

namespace IDT.RabbitMQ.Extensions
{
    public class ConfirmingPublisher
    {
        private readonly Lazy<IModel> _channel;

        public ConfirmingPublisher(IModel channel)
        {
            _channel = new Lazy<IModel>(() => ConfigureChannel(channel));
        }

        public void Publish(string exchangeName, IEnumerable<RabbitMessage> messages)
        {
            foreach (var rabbitMessage in messages)
            {
                _channel.Value.BasicPublish(exchangeName, rabbitMessage.RoutingKey, rabbitMessage.Properties, rabbitMessage.Body);
            }
            _channel.Value.WaitForConfirmsOrDie(TimeSpan.FromSeconds(1));
        }

        private IModel ConfigureChannel(IModel channel)
        {
            channel.ConfirmSelect();
            return channel;
        }
    }
}
