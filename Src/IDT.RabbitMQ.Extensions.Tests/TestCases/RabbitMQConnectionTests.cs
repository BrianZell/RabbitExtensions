using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace IDT.RabbitMQ.Extensions.Tests.TestCases
{
    class RabbitMQConnectionTests
    {
        [Test]
        public void Constructor_DoesNotCreateConnection()
        {
            var connection = Substitute.For<IConnection>();
            var factory = Substitute.For<IConnectionFactory>();
            factory.CreateConnection().Returns(connection);

            //Act
            new RabbitMqConnection(factory);
            
            //Assert
            factory.CreateConnection().DidNotReceiveWithAnyArgs();
        }

        [Test]
        public void AnyMethodCall_CreatesConnection()
        {
            var connection = Substitute.For<IConnection>();
            var factory = Substitute.For<IConnectionFactory>();
            factory.CreateConnection().Returns(connection);

            var sut = new RabbitMqConnection(factory);

            //Act
            sut.CreateModel();

            //Assert
            factory.Received().CreateConnection();
        }

        [Test]
        public void Dispose_DisposesConnection()
        {
            var connection = Substitute.For<IConnection>();
            var factory = Substitute.For<IConnectionFactory>();
            factory.CreateConnection().Returns(connection);

            var sut = new RabbitMqConnection(factory);

            //Act
            sut.CreateModel();
            sut.Dispose();

            //Assert
            connection.Received().Dispose();
        }

        [Test]
        public void Dispose_SwallowsExceptions()
        {
            var connection = Substitute.For<IConnection>();
            var factory = Substitute.For<IConnectionFactory>();
            factory.CreateConnection().Returns(connection);
            connection.When(x => x.Dispose()).Throw(new ChannelAllocationException());

            var sut = new RabbitMqConnection(factory);
            sut.CreateModel();

            //Act & Assert
            Assert.DoesNotThrow(() => sut.Dispose());
        }
    }
}
