using System;
using System.Collections.Generic;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace IDT.RabbitMQ.Extensions.Wrappers
{
    public abstract class ModelWrapper : IModel
    {
        public event EventHandler<BasicAckEventArgs> BasicAcks
        {
            add { InternalModel.BasicAcks += value; }
            remove { InternalModel.BasicAcks -= value; }
        }

        public event EventHandler<BasicNackEventArgs> BasicNacks
        {
            add { InternalModel.BasicNacks += value; }
            remove { InternalModel.BasicNacks -= value; }
        }

        public event EventHandler<EventArgs> BasicRecoverOk
        {
            add { InternalModel.BasicRecoverOk += value; }
            remove { InternalModel.BasicRecoverOk -= value; }
        }

        public event EventHandler<BasicReturnEventArgs> BasicReturn
        {
            add { InternalModel.BasicReturn += value; }
            remove { InternalModel.BasicReturn -= value; }
        }

        public event EventHandler<CallbackExceptionEventArgs> CallbackException
        {
            add { InternalModel.CallbackException += value; }
            remove { InternalModel.CallbackException -= value; }
        }

        public event EventHandler<FlowControlEventArgs> FlowControl
        {
            add { InternalModel.FlowControl += value; }
            remove { InternalModel.FlowControl -= value; }
        }

        public event EventHandler<ShutdownEventArgs> ModelShutdown
        {
            add { InternalModel.ModelShutdown += value; }
            remove { InternalModel.ModelShutdown += value; }
        }

        public int ChannelNumber
        {
            get { return InternalModel.ChannelNumber; }
        }

        public ShutdownEventArgs CloseReason
        {
            get { return InternalModel.CloseReason; }
        }

        public IBasicConsumer DefaultConsumer
        {
            get { return InternalModel.DefaultConsumer; }
            set { InternalModel.DefaultConsumer = value; }
        }

        public bool IsClosed
        {
            get { return InternalModel.IsClosed; }
        }

        public bool IsOpen
        {
            get { return InternalModel.IsOpen; }
        }

        public ulong NextPublishSeqNo
        {
            get { return InternalModel.NextPublishSeqNo; }
        }

        protected abstract IModel InternalModel { get; }

        public void Abort()
        {
            InternalModel.Abort();
        }

        public void Abort(ushort replyCode, string replyText)
        {
            InternalModel.Abort(replyCode, replyText);
        }

        public void BasicAck(ulong deliveryTag, bool multiple)
        {
            InternalModel.BasicAck(deliveryTag, multiple);
        }

        public void BasicCancel(string consumerTag)
        {
            InternalModel.BasicCancel(consumerTag);
        }

        public string BasicConsume(string queue, bool noAck, IBasicConsumer consumer)
        {
            return InternalModel.BasicConsume(queue, noAck, consumer);
        }

        public string BasicConsume(string queue, bool noAck, string consumerTag, IBasicConsumer consumer)
        {
            return InternalModel.BasicConsume(queue, noAck, consumerTag, consumer);
        }

        public string BasicConsume(string queue, bool noAck, string consumerTag, IDictionary<string, object> arguments, IBasicConsumer consumer)
        {
            return InternalModel.BasicConsume(queue, noAck, consumerTag, arguments, consumer);
        }

        public string BasicConsume(string queue, bool noAck, string consumerTag, bool noLocal, bool exclusive, IDictionary<string, object> arguments, IBasicConsumer consumer)
        {
            return InternalModel.BasicConsume(queue, noAck, consumerTag, noLocal, exclusive, arguments, consumer);
        }

        public BasicGetResult BasicGet(string queue, bool noAck)
        {
            return InternalModel.BasicGet(queue, noAck);
        }

        public void BasicNack(ulong deliveryTag, bool multiple, bool requeue)
        {
            InternalModel.BasicNack(deliveryTag, multiple, requeue);
        }

        public void BasicPublish(PublicationAddress addr, IBasicProperties basicProperties, byte[] body)
        {
            InternalModel.BasicPublish(addr, basicProperties, body);
        }

        public void BasicPublish(string exchange, string routingKey, IBasicProperties basicProperties, byte[] body)
        {
            InternalModel.BasicPublish(exchange, routingKey, basicProperties, body);
        }

        public void BasicPublish(string exchange, string routingKey, bool mandatory, IBasicProperties basicProperties, byte[] body)
        {
            InternalModel.BasicPublish(exchange, routingKey, mandatory, basicProperties, body);
        }

        public void BasicPublish(string exchange, string routingKey, bool mandatory, bool immediate, IBasicProperties basicProperties, byte[] body)
        {
            InternalModel.BasicPublish(exchange, routingKey, mandatory, immediate, basicProperties, body);
        }

        public void BasicQos(uint prefetchSize, ushort prefetchCount, bool global)
        {
            InternalModel.BasicQos(prefetchSize, prefetchCount, global);
        }

        public void BasicRecover(bool requeue)
        {
            InternalModel.BasicRecover(requeue);
        }

        public void BasicRecoverAsync(bool requeue)
        {
            InternalModel.BasicRecoverAsync(requeue);
        }

        public void BasicReject(ulong deliveryTag, bool requeue)
        {
            InternalModel.BasicReject(deliveryTag, requeue);
        }

        public void Close()
        {
            InternalModel.Close();
        }

        public void Close(ushort replyCode, string replyText)
        {
            InternalModel.Close(replyCode, replyText);
        }

        public void ConfirmSelect()
        {
            InternalModel.ConfirmSelect();
        }

        public IBasicProperties CreateBasicProperties()
        {
            return InternalModel.CreateBasicProperties();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void ExchangeBind(string destination, string source, string routingKey, IDictionary<string, object> arguments)
        {
            InternalModel.ExchangeBind(destination, source, routingKey, arguments);
        }

        public void ExchangeBind(string destination, string source, string routingKey)
        {
            InternalModel.ExchangeBind(destination, source, routingKey);
        }

        public void ExchangeBindNoWait(string destination, string source, string routingKey, IDictionary<string, object> arguments)
        {
            InternalModel.ExchangeBindNoWait(destination, source, routingKey, arguments);
        }

        public void ExchangeDeclare(string exchange, string type, bool durable, bool autoDelete, IDictionary<string, object> arguments)
        {
            InternalModel.ExchangeDeclare(exchange, type, durable, autoDelete, arguments);
        }

        public void ExchangeDeclare(string exchange, string type, bool durable)
        {
            InternalModel.ExchangeDeclare(exchange, type, durable);
        }

        public void ExchangeDeclare(string exchange, string type)
        {
            InternalModel.ExchangeDeclare(exchange, type);
        }

        public void ExchangeDeclareNoWait(string exchange, string type, bool durable, bool autoDelete, IDictionary<string, object> arguments)
        {
            InternalModel.ExchangeDeclareNoWait(exchange, type, durable, autoDelete, arguments);
        }

        public void ExchangeDeclarePassive(string exchange)
        {
            InternalModel.ExchangeDeclarePassive(exchange);
        }

        public void ExchangeDelete(string exchange, bool ifUnused)
        {
            InternalModel.ExchangeDelete(exchange, ifUnused);
        }

        public void ExchangeDelete(string exchange)
        {
            InternalModel.ExchangeDelete(exchange);
        }

        public void ExchangeDeleteNoWait(string exchange, bool ifUnused)
        {
            InternalModel.ExchangeDeleteNoWait(exchange, ifUnused);
        }

        public void ExchangeUnbind(string destination, string source, string routingKey, IDictionary<string, object> arguments)
        {
            InternalModel.ExchangeUnbind(destination, source, routingKey, arguments);
        }

        public void ExchangeUnbind(string destination, string source, string routingKey)
        {
            InternalModel.ExchangeUnbind(destination, source, routingKey);
        }

        public void ExchangeUnbindNoWait(string destination, string source, string routingKey, IDictionary<string, object> arguments)
        {
            InternalModel.ExchangeUnbindNoWait(destination, source, routingKey, arguments);
        }

        public void QueueBind(string queue, string exchange, string routingKey, IDictionary<string, object> arguments)
        {
            InternalModel.QueueBind(queue, exchange, routingKey, arguments);
        }

        public void QueueBind(string queue, string exchange, string routingKey)
        {
            InternalModel.QueueBind(queue, exchange, routingKey);
        }

        public void QueueBindNoWait(string queue, string exchange, string routingKey, IDictionary<string, object> arguments)
        {
            InternalModel.QueueBindNoWait(queue, exchange, routingKey, arguments);
        }

        public QueueDeclareOk QueueDeclare()
        {
            return InternalModel.QueueDeclare();
        }

        public QueueDeclareOk QueueDeclare(string queue, bool durable, bool exclusive, bool autoDelete, IDictionary<string, object> arguments)
        {
            return InternalModel.QueueDeclare(queue, durable, exclusive, autoDelete, arguments);
        }

        public void QueueDeclareNoWait(string queue, bool durable, bool exclusive, bool autoDelete, IDictionary<string, object> arguments)
        {
            InternalModel.QueueDeclareNoWait(queue, durable, exclusive, autoDelete, arguments);
        }

        public QueueDeclareOk QueueDeclarePassive(string queue)
        {
            return InternalModel.QueueDeclarePassive(queue);
        }

        public uint QueueDelete(string queue, bool ifUnused, bool ifEmpty)
        {
            return InternalModel.QueueDelete(queue, ifUnused, ifEmpty);
        }

        public uint QueueDelete(string queue)
        {
            return InternalModel.QueueDelete(queue);
        }

        public void QueueDeleteNoWait(string queue, bool ifUnused, bool ifEmpty)
        {
            InternalModel.QueueDeleteNoWait(queue, ifUnused, ifEmpty);
        }

        public uint QueuePurge(string queue)
        {
            return InternalModel.QueuePurge(queue);
        }

        public void QueueUnbind(string queue, string exchange, string routingKey, IDictionary<string, object> arguments)
        {
            InternalModel.QueueUnbind(queue, exchange, routingKey, arguments);
        }

        public void TxCommit()
        {
            InternalModel.TxCommit();
        }

        public void TxRollback()
        {
            InternalModel.TxRollback();
        }

        public void TxSelect()
        {
            InternalModel.TxSelect();
        }

        public bool WaitForConfirms()
        {
            return InternalModel.WaitForConfirms();
        }

        public bool WaitForConfirms(TimeSpan timeout)
        {
            return InternalModel.WaitForConfirms(timeout);
        }

        public bool WaitForConfirms(TimeSpan timeout, out bool timedOut)
        {
            return InternalModel.WaitForConfirms(timeout, out timedOut);
        }

        public void WaitForConfirmsOrDie()
        {
            InternalModel.WaitForConfirmsOrDie();
        }

        public void WaitForConfirmsOrDie(TimeSpan timeout)
        {
            InternalModel.WaitForConfirmsOrDie(timeout);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                InternalModel.Dispose();
            }
        }
    }
}