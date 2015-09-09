using System;
using System.Threading;
using System.Threading.Tasks;
using IDT.RabbitMQ.Extensions.Internals;

namespace IDT.RabbitMQ.Extensions
{
    public interface IMessageHandler
    {
        Task HandleMessage(DeliveredRabbitMessage message, CancellationToken cancellationToken);
    }
}