using System;
using IDT.RabbitMQ.Extensions.Internals;
using IDT.RabbitMQ.Extensions.Wrappers;
using RabbitMQ.Client;

namespace IDT.RabbitMQ.Extensions
{
    public class RabbitMqConnection : ConnectionWrapper
    {
        public RabbitMqConnection(IConnectionFactory connectionFactory)
        {
            _connection = new Lazy<RabbitExceptionSnare<IConnection>>(() => new RabbitExceptionSnare<IConnection>(connectionFactory.CreateConnection()));
        }

        protected override IConnection InternalConnection
        {
            get { return _connection.Value.Value; }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _connection.IsValueCreated)
            {
                _connection.Value.Dispose();
            }
        }

        private readonly Lazy<RabbitExceptionSnare<IConnection>> _connection;
    }
}