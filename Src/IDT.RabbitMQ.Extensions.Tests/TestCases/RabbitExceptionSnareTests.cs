using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDT.RabbitMQ.Extensions.Internals;
using NSubstitute;
using NUnit.Framework;
using RabbitMQ.Client.Exceptions;

namespace IDT.RabbitMQ.Extensions.Tests.TestCases
{
    public class RabbitErrorSnareTests
    {
        #region Test Methods

        [Test]
        public void Execute_WhenNoErrors_Executes()
        {
            //Arrange
            bool called = false;
            var disposable = Substitute.For<IDisposable>();
            disposable.When(x => x.Dispose()).Do(c => called = true);
            var snare = new RabbitExceptionSnare<IDisposable>(disposable);

            //Act
            snare.Dispose();

            //Assert
            Assert.That(called, Is.True);
        }

        [Test]
        public void Execute_WhenNonRabbitError_Throws()
        {

            //Arrange
            var disposable = Substitute.For<IDisposable>();
            disposable.When(x => x.Dispose()).Do(c => { throw new Exception(); });
            var snare = new RabbitExceptionSnare<IDisposable>(disposable);

            //Act & Assert
            Assert.Throws<Exception>(snare.Dispose);
        }

        [Test]
        public void Execute_WhenRabbitError_DoesNotThrow()
        {
            //Arrange
            var disposable = Substitute.For<IDisposable>();
            disposable.When(x => x.Dispose()).Do(c => { throw new ChannelAllocationException(); });
            var snare = new RabbitExceptionSnare<IDisposable>(disposable);

            //Act & Assert
            Assert.DoesNotThrow(snare.Dispose);
        }

        #endregion
    }
}
