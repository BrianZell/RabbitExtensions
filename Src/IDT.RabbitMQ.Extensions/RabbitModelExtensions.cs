using IDT.RabbitMQ.Extensions.Internals;
using RabbitMQ.Client;

namespace IDT.RabbitMQ.Extensions
{
    public static class ModelExtensions
    {
        public static AsyncMessageConsumer HandleMessagesAsync(this IModel source, string queueName)
        {
            var messageSource = new ConfirmingSubscription(source);
            var consumer = new AsyncMessageConsumer(messageSource);
            messageSource.Subscribe(queueName);
            return consumer;
        }
    }
}
