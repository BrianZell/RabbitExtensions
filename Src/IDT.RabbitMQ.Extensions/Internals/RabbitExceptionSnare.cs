using System;
using System.IO;
using System.Net;
using RabbitMQ.Client.Exceptions;

namespace IDT.RabbitMQ.Extensions.Internals
{
    public class RabbitExceptionSnare<T> : IDisposable
        where T : IDisposable
    {
        private readonly T _item;

        public RabbitExceptionSnare(T item)
        {
            _item = item;
        }

        public T Value 
        {
            get { return _item; }
        }

        public void Dispose()
        {
            RabbitExceptionSnare.Execute(_item.Dispose);
        }
    }

    public static class RabbitExceptionSnare
    {
        public static void Execute(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                if (!(ex is OperationInterruptedException ||
                      ex is ProtocolViolationException ||
                      ex is ChannelAllocationException ||
                      ex is PossibleAuthenticationFailureException ||
                      ex is IOException ||
                      ex is ObjectDisposedException ||
                      ex is NotSupportedException))
                {
                    throw;
                }

                //swallow all named exceptions
            }
        }
    }
}