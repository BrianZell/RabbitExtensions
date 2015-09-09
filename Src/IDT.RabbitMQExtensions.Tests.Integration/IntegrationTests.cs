using IDT.RabbitMQ.Extensions;
using NUnit.Framework;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IDT.RabbitMQExtensions.Tests.Integration
{
    public class IntegrationTests
    {
        [Test,Ignore]
        public async Task SendAndRecieveMessage()
        {
            var queueName = "testqueueB";
            var message = new RabbitMessage() { RoutingKey = queueName, Body = new byte[]{0,1,2,3,4}};
            var messageHandler = new TestMessageHandler();

            var connectionFactory = new ConnectionFactory
                                        {
                                            HostName = "mqBalancer",
                                            UserName = "vault",
                                            Password = "crystal",
                                            RequestedHeartbeat = 5,
                                        };

            using (var connection = new RabbitMqConnection(connectionFactory))
            {
                using (var model = new RabbitMqModel(connection))
                {
                    model.QueueDeclare(queueName, false, true, false, new Dictionary<string, object>());

                    var consumer = model.HandleMessagesAsync(queueName);
                    var publisher = new ConfirmingPublisher(model);
                    await Task.Delay(TimeSpan.FromSeconds(1));
                    publisher.Publish(string.Empty, new[] { message });
                    await consumer.ConsumeMessages(messageHandler, CancellationToken.None);
                }
            }

            var result = messageHandler.LastMessage;
            Assert.That(result.Body, Is.EquivalentTo(message.Body));
        }

        public class TestMessageHandler : IMessageHandler
        {
            public RabbitMessage LastMessage;

            public async Task HandleMessage(DeliveredRabbitMessage message, CancellationToken cancellationToken)
            {
                LastMessage = message;
            }
        }
    }
}
