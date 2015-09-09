using System;
using IDT.RabbitMQ.Extensions.Internals;
using IDT.RabbitMQ.Extensions.Wrappers;
using RabbitMQ.Client;

namespace IDT.RabbitMQ.Extensions
{
    public class RabbitMqModel : ModelWrapper
    {
        public RabbitMqModel(IConnection connection)
        {
            _model = new Lazy<RabbitExceptionSnare<IModel>>(() => new RabbitExceptionSnare<IModel>(connection.CreateModel()));
        }

        protected override IModel InternalModel
        {
            get { return _model.Value.Value; }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _model.IsValueCreated)
            {
                _model.Value.Dispose();
            }
        }

        private readonly Lazy<RabbitExceptionSnare<IModel>> _model;
    }
}