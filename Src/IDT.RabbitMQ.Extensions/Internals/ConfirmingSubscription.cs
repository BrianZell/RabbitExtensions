using RabbitMQ.Client;
using System.Collections.Concurrent;

namespace IDT.RabbitMQ.Extensions.Internals
{
    public class ConfirmingSubscription : ConfirmingMessageSource
    {
        private readonly IModel _model;
        private readonly ConcurrentQueue<DeliveredRabbitMessage> _queue = new ConcurrentQueue<DeliveredRabbitMessage>();

        private string _consumerTag;
        private string _queueName;

        public ConfirmingSubscription(IModel model)
            : base(model)
        {
            _model = model;
        }

        public string QueueName
        {
            get { return _queueName; }
        }

        public void Subscribe(string queueName)
        {
            _consumerTag = _model.BasicConsume(queueName, false, new ConcurrentQueueBasicConsumer(_queue));
            _queueName = queueName;
        }

        public void Unsubscribe()
        {
            if (_consumerTag != null)
            {
                _model.BasicCancel(_consumerTag);
                _consumerTag = null;
            }
        }

        public override bool TryGetNextMessage(out DeliveredRabbitMessage message)
        {
            return _queue.TryDequeue(out message);
        }        
    }
}
