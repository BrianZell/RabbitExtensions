using System;
using System.Threading;
using System.Threading.Tasks;
using IDT.RabbitMQ.Extensions;
using IDT.RabbitMQ.Extensions.Internals;
using IDT.TaskActions;
using log4net;
using RabbitMQ.Client;

namespace IDT.RabbitMQ.ServiceClient
{
    public abstract class RabbitSubscriberTaskAction : ITaskAction, IMessageHandler
    {
        public readonly ILog _log = LogManager.GetLogger("IDT.RabbitMQ.ServiceClient.RabbitSubscriberTaskAction");

        private readonly IModel _model;
        private readonly string _queueName;

        protected RabbitSubscriberTaskAction(IModel model, string queueName)
        {
            _model = model;
            _queueName = queueName;
        }

        public virtual int ConcurrencyLevel
        {
            get { return AsyncMessageConsumer.DefaultConcurrencyLevel; }
        }

        public virtual TimeSpan RestTime
        {
            get { return TimeSpan.FromSeconds(5); }
        }

        public async Task RunAction(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var messageSource = new ConfirmingSubscription(_model);
            messageSource.Subscribe(_queueName);
            var consumer = new AsyncMessageConsumer(new LoggingMessageSourceDecorator(messageSource))
                               {
                                   ConcurrencyLevel = this.ConcurrencyLevel
                               };

            _log.Debug(string.Format("Consumer started for queue '{0}'", _queueName));
            while (_model.IsOpen && !cancellationToken.IsCancellationRequested)
            {
                await consumer.ConsumeMessages(this, cancellationToken);
                await Task.Delay(RestTime, cancellationToken);
            }
            _log.Debug(string.Format("Consumer stopped for queue '{0}'", _queueName));
        }

        public abstract Task HandleMessage(DeliveredRabbitMessage message, CancellationToken cancellationToken);
    }
}
