using System;
using System.Collections.Generic;
using System.Net;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace IDT.RabbitMQ.Extensions.Wrappers
{
    public abstract class ConnectionWrapper : IConnection
    {
        public event EventHandler<CallbackExceptionEventArgs> CallbackException
        {
            add { InternalConnection.CallbackException += value; }
            remove { InternalConnection.CallbackException -= value; }
        }

        public event EventHandler<ConnectionBlockedEventArgs> ConnectionBlocked
        {
            add { InternalConnection.ConnectionBlocked += value; }
            remove { InternalConnection.ConnectionBlocked -= value; }
        }

        public event EventHandler<ShutdownEventArgs> ConnectionShutdown
        {
            add { InternalConnection.ConnectionShutdown += value; }
            remove { InternalConnection.ConnectionShutdown -= value; }
        }

        public event EventHandler<EventArgs> ConnectionUnblocked
        {
            add { InternalConnection.ConnectionUnblocked += value; }
            remove { InternalConnection.ConnectionUnblocked -= value; }
        }

        public bool AutoClose
        {
            get { return InternalConnection.AutoClose; }
            set { InternalConnection.AutoClose = value; }
        }

        public ushort ChannelMax
        {
            get { return InternalConnection.ChannelMax; }
        }

        public IDictionary<string, object> ClientProperties
        {
            get { return InternalConnection.ClientProperties; }
        }

        public ShutdownEventArgs CloseReason
        {
            get { return InternalConnection.CloseReason; }
        }

        public ConsumerWorkService ConsumerWorkService
        {
            get { return InternalConnection.ConsumerWorkService; }
        }

        public AmqpTcpEndpoint Endpoint
        {
            get { return InternalConnection.Endpoint; }
        }

        public uint FrameMax
        {
            get { return InternalConnection.FrameMax; }
        }

        public ushort Heartbeat
        {
            get { return InternalConnection.Heartbeat; }
        }

        public bool IsOpen
        {
            get { return InternalConnection.IsOpen; }
        }

        public AmqpTcpEndpoint[] KnownHosts
        {
            get { return InternalConnection.KnownHosts; }
        }

        public EndPoint LocalEndPoint
        {
            get { return InternalConnection.LocalEndPoint; }
        }

        public int LocalPort
        {
            get { return InternalConnection.LocalPort; }
        }

        public IProtocol Protocol
        {
            get { return InternalConnection.Protocol; }
        }

        public EndPoint RemoteEndPoint
        {
            get { return InternalConnection.RemoteEndPoint; }
        }

        public int RemotePort
        {
            get { return InternalConnection.RemotePort; }
        }

        public IDictionary<string, object> ServerProperties
        {
            get { return InternalConnection.ServerProperties; }
        }

        public IList<ShutdownReportEntry> ShutdownReport
        {
            get { return InternalConnection.ShutdownReport; }
        }

        protected abstract IConnection InternalConnection { get; }

        public void Abort()
        {
            InternalConnection.Abort();
        }

        public void Abort(ushort reasonCode, string reasonText)
        {
            InternalConnection.Abort(reasonCode, reasonText);
        }

        public void Abort(int timeout)
        {
            InternalConnection.Abort(timeout);
        }

        public void Abort(ushort reasonCode, string reasonText, int timeout)
        {
            InternalConnection.Abort(reasonCode, reasonText, timeout);
        }

        public void Close()
        {
            InternalConnection.Close();
        }

        public void Close(ushort reasonCode, string reasonText)
        {
            InternalConnection.Close(reasonCode, reasonText);
        }

        public void Close(int timeout)
        {
            InternalConnection.Close(timeout);
        }

        public void Close(ushort reasonCode, string reasonText, int timeout)
        {
            InternalConnection.Close(reasonCode, reasonText, timeout);
        }

        public IModel CreateModel()
        {
            return InternalConnection.CreateModel();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void HandleConnectionBlocked(string reason)
        {
            InternalConnection.HandleConnectionBlocked(reason);
        }

        public void HandleConnectionUnblocked()
        {
            InternalConnection.HandleConnectionUnblocked();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                InternalConnection.Dispose();
            }
        }
    }
}