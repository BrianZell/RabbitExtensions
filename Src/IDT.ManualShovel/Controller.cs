using System;
using System.Collections.Generic;
using System.Linq;
using IDT.RabbitMQ.Extensions;
using IDT.RabbitMQ.Extensions.Internals;
using RabbitMQ.Client;

namespace IDT.ManualShovel
{
    public class Controller : IDisposable
    {
        private readonly string _queueName;
        private RabbitMqConnection _sourceConnection;
        private RabbitMqModel _sourceChannel;
        private IMessageSource _messageSource;
        private ConfirmingPublisher _messagePublisher;

        public Controller(IConnectionFactory sourceConnectionFactory, string queueName)
        {
            _queueName = queueName;
            _sourceConnection = new RabbitMqConnection(sourceConnectionFactory);
            _sourceChannel = new RabbitMqModel(_sourceConnection);
            _sourceChannel.QueueDeclarePassive(queueName);
            _messageSource = new ConfirmingGetter(_sourceChannel) { QueueName = queueName };
            _messagePublisher = new ConfirmingPublisher(_sourceChannel);
        }

        public IEnumerable<MessageView> RequeueItemsAndGetMore(IEnumerable<MessageView> currentMessageViews)
        {
            var currentMessageViewList = currentMessageViews.ToList();
            foreach (var view in currentMessageViewList)
            {
                var message = view.ProvideOriginalMessage();
                message.RoutingKey = _queueName;
                _messagePublisher.Publish(string.Empty, new[] { message });
                _messageSource.Acknowledge(message);
            }

            return GetAvailableMessages(currentMessageViewList.Count);
        }

        public IList<MessageView> SendToDestination(IEnumerable<MessageView> viewsToSend, string queueName)
        {
            var viewsToSendList = viewsToSend.ToList();
            using (var publishChannel = new RabbitMqModel(_sourceConnection))
            {
                try
                {
                    publishChannel.QueueDeclarePassive(queueName);
                }
                catch (Exception ex)
                {
                    throw new InvalidQueueException(string.Format("Could not find queue '{0}'",queueName),ex);
                }
                
                var publisher = new ConfirmingPublisher(publishChannel);
                foreach (var view in viewsToSendList)
                {
                    var message = view.ProvideOriginalMessage();
                    message.RoutingKey = queueName;
                    message.Properties.Timestamp = DateTime.Now.ToAmqpTimestamp();
                    publisher.Publish(string.Empty, new[] {message});
                    _messageSource.Acknowledge(message);
                }
            }

            return GetAvailableMessages(viewsToSendList.Count);
        }

        public IList<MessageView> GetAvailableMessages(int count)
        {
            var results = new List<MessageView>();
            DeliveredRabbitMessage deliveredRabbitMessage;
            while (results.Count < count && _messageSource.TryGetNextMessage(out deliveredRabbitMessage))
            {
                results.Add(new MessageView(deliveredRabbitMessage));
            }

            return results;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_sourceConnection != null)
                {
                    _sourceConnection.Dispose();
                    _sourceConnection = null;
                }

                if (_sourceChannel != null)
                {
                    _sourceChannel.Dispose();
                    _sourceChannel = null;
                }
            }
        }
    }
}
