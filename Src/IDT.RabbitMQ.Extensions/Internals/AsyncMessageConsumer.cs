using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IDT.RabbitMQ.Extensions.Internals
{
    public class AsyncMessageConsumer
    {
        public const int DefaultConcurrencyLevel = 4;

        private readonly IMessageSource _messageSource;

        private int _concurrencyLevel = DefaultConcurrencyLevel;

        public AsyncMessageConsumer(IMessageSource messageSource)
        {
            _messageSource = messageSource;
        }

        public int ConcurrencyLevel 
        {
            get { return _concurrencyLevel; }
            set
            {
                if (_concurrencyLevel < 1)
                {
                    throw new ArgumentException("ConcurrencyLevel must be a positive integer");
                }

                _concurrencyLevel = value;
            }
        }

        public async Task ConsumeMessages(IMessageHandler handler, CancellationToken cancellationToken)
        {
            var concurrencyLevel = ConcurrencyLevel;
            var messagesInProcess = new Dictionary<Task, DeliveredRabbitMessage>();
            do
            {
                if (messagesInProcess.Any())
                {
                    var completedMessage = await Task.WhenAny(messagesInProcess.Keys);
                    var originalMessage = messagesInProcess[completedMessage];
                    messagesInProcess.Remove(completedMessage);
                    try
                    {
                        await completedMessage;
                        _messageSource.Acknowledge(originalMessage);
                    }
                    catch (OperationCanceledException)
                    {
                        _messageSource.Cancel(originalMessage);
                    }
                    catch (Exception exception)
                    {
                        _messageSource.Failure(originalMessage, exception);
                    }
                }

                DeliveredRabbitMessage nextMessage;
                while (!cancellationToken.IsCancellationRequested 
                    && messagesInProcess.Count < concurrencyLevel 
                    && _messageSource.TryGetNextMessage(out nextMessage))
                {
                    try
                    {
                        var task = handler.HandleMessage(nextMessage, cancellationToken);
                        messagesInProcess.Add(task, nextMessage);
                    }
                    catch (Exception exception)
                    {
                        _messageSource.Failure(nextMessage, exception);
                    }
                }
            } while (messagesInProcess.Any());
        }
    }
}
