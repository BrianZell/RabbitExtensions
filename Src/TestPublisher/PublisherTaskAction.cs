using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IDT.RabbitMQ.Extensions;
using IDT.TaskActions;
using RabbitMQ.Client;

namespace TestPublisher
{
    class PublisherTaskAction : ITaskAction
    {
        private readonly IModel _model;

        public PublisherTaskAction(IModel model)
        {
            _model = model;
            _model.QueueDeclare("testqueue2", true, false, false, new Dictionary<string, object>());
        }

        public async Task RunAction(CancellationToken cancellationToken)
        {
            var publisher = new ConfirmingPublisher(_model);
            var first = "Start! ";
            while (_model.IsOpen)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var messageBody = first + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
                publisher.Publish(string.Empty, new[] { new RabbitMessage { Body = Encoding.UTF8.GetBytes(messageBody), RoutingKey = "testqueue2" } });
                first = string.Empty;
                await Task.Delay(TimeSpan.FromHours(1), cancellationToken);
            }
        }
    }
}
