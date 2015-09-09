using NSubstitute;
using NUnit.Framework;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace IDT.RabbitMQ.Extensions.Tests.TestCases
{
    class RabbitMQModelTests
    {
        public void Constructor_DoesNotCreateModel()
        {
            var connection = Substitute.For<IConnection>();
            var model = Substitute.For<IModel>();
            connection.CreateModel().Returns(model);

            //Act
            new RabbitMqModel(connection);

            //Assert
            connection.CreateModel().DidNotReceiveWithAnyArgs();
        }

        [Test]
        public void AnyMethodCall_CreatesModel()
        {
            var connection = Substitute.For<IConnection>();
            var model = Substitute.For<IModel>();
            connection.CreateModel().Returns(model);

            var sut = new RabbitMqModel(connection);

            //Act
            sut.ConfirmSelect();

            //Assert
            connection.Received().CreateModel();
        }

        [Test]
        public void Dispose_DisposesConnection()
        {
            var connection = Substitute.For<IConnection>();
            var model = Substitute.For<IModel>();
            connection.CreateModel().Returns(model);

            var sut = new RabbitMqModel(connection);

            //Act
            sut.ConfirmSelect();
            sut.Dispose();

            //Assert
            model.Received().Dispose();
        }

        [Test]
        public void Dispose_SwallowsExceptions()
        {
            var connection = Substitute.For<IConnection>();
            var model = Substitute.For<IModel>();
            connection.CreateModel().Returns(model);
            model.When(x => x.Dispose()).Throw(new ChannelAllocationException());

            var sut = new RabbitMqModel(connection);
            sut.ConfirmSelect();

            //Act & Assert
            Assert.DoesNotThrow(() => sut.Dispose());
        }
    }
}
