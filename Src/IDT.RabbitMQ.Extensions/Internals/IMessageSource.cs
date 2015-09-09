using System;

namespace IDT.RabbitMQ.Extensions.Internals
{
    public interface IMessageSource
    {
        bool TryGetNextMessage(out DeliveredRabbitMessage message);
        void Acknowledge(DeliveredRabbitMessage message);
        void Failure(DeliveredRabbitMessage message, Exception exception);
        void Cancel(DeliveredRabbitMessage message);
    }
}