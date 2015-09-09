using System;
using IDT.RabbitMQ.Extensions;
using IDT.RabbitMQ.Extensions.Internals;
using log4net;

namespace IDT.RabbitMQ.ServiceClient
{
    public class LoggingMessageSourceDecorator : IMessageSource
    {
        private readonly IMessageSource _messageSource;
        private readonly ILog _log = LogManager.GetLogger("IDT.RabbitMQ.Extensions.IMessageSource");

        public LoggingMessageSourceDecorator(IMessageSource messageSource)
        {
            _messageSource = messageSource;
        }

        public bool TryGetNextMessage(out DeliveredRabbitMessage message)
        {
            var result = _messageSource.TryGetNextMessage(out message);
            if (result && _log.IsDebugEnabled)
            {
                _log.Debug(string.Format("Recieved message with Delivery tag '{0}': {1}", message.DeliveryTag, message));
            }
            return result;
        }

        public void Acknowledge(DeliveredRabbitMessage message)
        {
            if (_log.IsDebugEnabled)
            {
                _log.Debug(string.Format("Acknowledging message with delivery tag '{0}'", message.DeliveryTag));
            }
            _messageSource.Acknowledge(message);
        }

        public void Cancel(DeliveredRabbitMessage message)
        {
            if (_log.IsDebugEnabled)
            {
                _log.Debug(string.Format("Canceling message processing with delivery tag '{0}'", message.DeliveryTag));
            }
            _messageSource.Cancel(message);
        }

        public void Failure(DeliveredRabbitMessage message, Exception exception)
        {
            if (_log.IsErrorEnabled)
            {
                _log.Error(string.Format("Failure while processing message '{0}'.", message), exception);
            }
            _messageSource.Failure(message, exception);
        }
    }
}
