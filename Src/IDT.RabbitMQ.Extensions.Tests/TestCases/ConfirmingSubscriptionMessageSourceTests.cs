using System;
using IDT.RabbitMQ.Extensions.Internals;
using NSubstitute;
using NUnit.Framework;
using RabbitMQ.Client;
using RabbitMQ.Client.Framing;

namespace IDT.RabbitMQ.Extensions.Tests.TestCases
{
    class ConfirmingSubscriptionMessageSourceTests
    {
        [Test]
        public void TryGetNextMessage_WhenThereIsAMessage_ReturnsTrue()
        {
            //Arrange
            IBasicConsumer basicConsumer = null;
            var model = Substitute.For<IModel>();
            model.BasicConsume("queueName", false, Arg.Do<IBasicConsumer>(x => basicConsumer = x));

            var sut = new ConfirmingSubscription(model);
            sut.Subscribe("queueName");
            
            //Act
            basicConsumer.HandleBasicDeliver(string.Empty,1,false,string.Empty,string.Empty,new BasicProperties(), new byte[0]);
            DeliveredRabbitMessage message = null;
            bool result = sut.TryGetNextMessage(out message);

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void TryGetNextMessage_WhenThereIsAMessage_ReturnsValues()
        {
            //Arrange
            ulong deliveryTag = 1;
            string routingKey = "routingKey";
            byte[] byteArray = {0,1,0};
            IBasicProperties basicProperties = new BasicProperties();;
            IBasicConsumer basicConsumer = null;
            var model = Substitute.For<IModel>();
            model.BasicConsume("queueName", false, Arg.Do<IBasicConsumer>(x => basicConsumer = x));

            var sut = new ConfirmingSubscription(model);
            sut.Subscribe("queueName");

            //Act
            basicConsumer.HandleBasicDeliver(string.Empty, deliveryTag, false, string.Empty, routingKey, basicProperties, byteArray);
            DeliveredRabbitMessage message = null;
            bool result = sut.TryGetNextMessage(out message);

            //Assert
            Assert.That(message.DeliveryTag, Is.EqualTo(deliveryTag));
            Assert.That(message.RoutingKey, Is.EqualTo(routingKey));
            Assert.That(message.Body, Is.EqualTo(byteArray));
            Assert.That(message.Properties, Is.EqualTo(basicProperties));
        }

        [Test]
        public void TryGetNextMessage_WhenThereIsNoMessage_ReturnsFalse()
        {
            //Arrange
            IBasicConsumer basicConsumer = null;
            var model = Substitute.For<IModel>();
            model.BasicConsume("queueName", false, Arg.Do<IBasicConsumer>(x => basicConsumer = x));

            var sut = new ConfirmingSubscription(model);
            sut.Subscribe("queueName");

            //Act
            DeliveredRabbitMessage message = null;
            bool result = sut.TryGetNextMessage(out message);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void Acknowledge_CallsModelBasicAck()
        {
            //Arrange
            ulong deliveryTag = 1;
            var message = new DeliveredRabbitMessage { DeliveryTag = deliveryTag };
            var model = Substitute.For<IModel>();
            var sut = new ConfirmingSubscription(model);
            
            //Act
            sut.Acknowledge(message);

            //Assert
            model.Received().BasicAck(deliveryTag,false);
        }

        [Test]
        public void Failure_CallsModelBasicNack()
        {
            //Arrange
            ulong deliveryTag = 1;
            var message = new DeliveredRabbitMessage { DeliveryTag = deliveryTag };
            var model = Substitute.For<IModel>();
            var sut = new ConfirmingSubscription(model);
            
            //Act
            sut.Failure(message,new Exception());

            //Assert
            model.Received().BasicNack(deliveryTag, false, false);
        }
    }
}
