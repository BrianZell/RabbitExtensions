using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IDT.RabbitMQ.Extensions.Internals;
using NSubstitute;
using NUnit.Framework;

namespace IDT.RabbitMQ.Extensions.Tests.TestCases
{
    class AsyncMessageConsumerTests
    {
        [Test]
        public async Task ConsumeMessages_HandlesMessagesFromSource()
        {
            var message1 = new DeliveredRabbitMessage();
            var message2 = new DeliveredRabbitMessage();
            var source = new MockMessageSource(new [] {message1,message2});
            var handler = Substitute.For<IMessageHandler>();
            handler.HandleMessage(message1, Arg.Any<CancellationToken>())
                   .Returns(Task.Delay(0));
            
            var sut = new AsyncMessageConsumer(source) { ConcurrencyLevel = 2 };

            //Act
            await sut.ConsumeMessages(handler, CancellationToken.None);

            //Assert
            handler.Received().HandleMessage(message1, Arg.Any<CancellationToken>()).IgnoreAwait();
            handler.Received().HandleMessage(message2, Arg.Any<CancellationToken>()).IgnoreAwait();
        }

        [Test]
        public async Task ConsumeMessages_WhenCompletesSuccessfully_AcksMessages()
        {
            var message1 = new DeliveredRabbitMessage();
            var message2 = new DeliveredRabbitMessage();
            var source = new MockMessageSource(new [] { message1, message2 });
            var handler = Substitute.For<IMessageHandler>();
            handler.HandleMessage(message1, Arg.Any<CancellationToken>())
                   .Returns(Task.Delay(0));

            var sut = new AsyncMessageConsumer(source) { ConcurrencyLevel = 2 };

            //Act
            await sut.ConsumeMessages(handler, CancellationToken.None);

            //Assert
            Assert.That(source.Acks.Contains(message1));
            Assert.That(source.Acks.Contains(message2));
        }

        [Test]
        public async Task ConsumeMessages_WhenHandlerThrows_NacksMessage()
        {
            var message1 = new DeliveredRabbitMessage();
            var source = new MockMessageSource(new [] { message1, new DeliveredRabbitMessage() });
            var exception = new Exception();
            var handler = Substitute.For<IMessageHandler>();
            handler.When(x => x.HandleMessage(message1, Arg.Any<CancellationToken>()))
                .Do(x => { throw exception; });

            var sut = new AsyncMessageConsumer(source) { ConcurrencyLevel = 2 };

            //Act
            await sut.ConsumeMessages(handler, CancellationToken.None);

            //Assert
            Assert.That(source.Nacks.Contains(new KeyValuePair<DeliveredRabbitMessage, Exception>(message1, exception)));
        }

        [Test]
        public async Task ConsumeMessages_WhenHandlerThrows_AcksOtherMessage()
        {
            var message1 = new DeliveredRabbitMessage();
            var message2 = new DeliveredRabbitMessage();
            var source = new MockMessageSource(new [] { message1, message2 });
            var handler = Substitute.For<IMessageHandler>();
            handler.When(x => x.HandleMessage(message1, Arg.Any<CancellationToken>()))
                .Do(x => { throw new Exception(); });

            var sut = new AsyncMessageConsumer(source) { ConcurrencyLevel = 2 };

            //Act
            await sut.ConsumeMessages(handler,CancellationToken.None);

            //Assert
            Assert.That(source.Acks.Contains(message2));
        }

        [Test]
        public async Task ConsumeMessages_WhenHandlerThrowsAsync_NacksMessage()
        {
            var message1 = new DeliveredRabbitMessage();
            var source = new MockMessageSource(new [] { message1, new DeliveredRabbitMessage()});
            var exception = new Exception();
            var handler = Substitute.For<IMessageHandler>();
            handler.HandleMessage(message1, Arg.Any<CancellationToken>())
                .Returns(y => Task.Run(async () =>{
                                  await Task.Yield();
                                  throw exception; 
                              }));

            var sut = new AsyncMessageConsumer(source) { ConcurrencyLevel = 2 };

            //Act
            await sut.ConsumeMessages(handler,CancellationToken.None);

            //Assert
            Assert.That(source.Nacks.Contains(new KeyValuePair<DeliveredRabbitMessage, Exception>(message1, exception)));
        }

        [Test]
        public async Task ConsumeMessages_WhenHandlerThrowsAsync_AcksOtherMessage()
        {
            var message1 = new DeliveredRabbitMessage();
            var message2 = new DeliveredRabbitMessage();
            var source = new MockMessageSource(new [] { message1, message2 });
            var handler = Substitute.For<IMessageHandler>();
            handler.HandleMessage(message1, Arg.Any<CancellationToken>())
                .Returns(y => Task.Run(async () =>
                {
                    await Task.Yield();
                    throw new Exception();
                }));

            var sut = new AsyncMessageConsumer(source) { ConcurrencyLevel = 2 };

            //Act
            await sut.ConsumeMessages(handler,CancellationToken.None);

            //Assert
            Assert.That(source.Acks.Contains(message2));
        }

        [Test]
        public async Task ConsumeMessages_WhenAlreadyCancelled_DoesNothing()
        {
            var message1 = new DeliveredRabbitMessage();
            var message2 = new DeliveredRabbitMessage();
            var source = new MockMessageSource(new[] { message1, message2 });
            var handler = Substitute.For<IMessageHandler>();
            handler.HandleMessage(message1, Arg.Any<CancellationToken>())
                   .Returns(Task.Delay(0));

            var sut = new AsyncMessageConsumer(source) { ConcurrencyLevel = 2 };

            using (var cts = new CancellationTokenSource())
            {
                cts.Cancel();

                //Act
                await sut.ConsumeMessages(handler, cts.Token);
            }

            //Assert
            handler.DidNotReceive().HandleMessage(message1, Arg.Any<CancellationToken>()).IgnoreAwait();
            handler.DidNotReceive().HandleMessage(message2, Arg.Any<CancellationToken>()).IgnoreAwait();
        }

        [Test]
        public async Task ConsumeMessages_WhenHandlersAreCancelled_DoesNotAcknowledgeOrFail()
        {
            var message = new DeliveredRabbitMessage();
            var source = new MockMessageSource(new[] { message });
            var handler = Substitute.For<IMessageHandler>();
            var sut = new AsyncMessageConsumer(source);

            handler.HandleMessage(Arg.Any<DeliveredRabbitMessage>(), Arg.Any<CancellationToken>())
                       .Returns(c => Task.Delay(-1, c.Arg<CancellationToken>()));

            using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(0.1)))
            {
                //Act
                await sut.ConsumeMessages(handler, cts.Token);
            }

            //Assert
            Assert.That(source.Acks.Any(), Is.False);
            Assert.That(source.Nacks.Any(), Is.False);
            Assert.That(source.Canceled.Contains(message));
        }
    }

    public static class AsyncWarningExt
    {
        public static void IgnoreAwait(this Task task)
        {
        }
    }
}
