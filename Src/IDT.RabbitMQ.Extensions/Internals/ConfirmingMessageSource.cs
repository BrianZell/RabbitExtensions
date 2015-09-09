using System;
using RabbitMQ.Client;

namespace IDT.RabbitMQ.Extensions.Internals
{
    public abstract class ConfirmingMessageSource : IMessageSource
    {
        private readonly IModel _model;
        
        protected ConfirmingMessageSource(IModel model)
        {
            _model = model;
        }

        public abstract bool TryGetNextMessage(out DeliveredRabbitMessage message);

        public virtual void Acknowledge(DeliveredRabbitMessage message)
        {
            _model.BasicAck(message.DeliveryTag, false);
        }

        public virtual void Cancel(DeliveredRabbitMessage message)
        {
            _model.BasicNack(message.DeliveryTag, false, true);
        }

        public virtual void Failure(DeliveredRabbitMessage message, Exception exception)
        {
            _model.BasicNack(message.DeliveryTag, false, false);
        }
    }
}