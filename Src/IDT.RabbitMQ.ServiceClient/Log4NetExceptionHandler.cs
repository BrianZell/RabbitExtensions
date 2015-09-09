using System;
using IDT.TaskActions;
using log4net;

namespace IDT.RabbitMQ.ServiceClient
{
    public class Log4NetExceptionHandler : IExceptionHandler
    {
        private readonly ILog _log = LogManager.GetLogger("IDT.RabbitMQ.ServiceClient.ExceptionHandler");

        public void HandleException(Exception exception)
        {
            _log.Error("Unhandled Exception", exception);
        }
    }
}