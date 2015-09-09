using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace IDT.RabbitMQ.Extensions.Internals
{
    public class ConfirmingGetter : ConfirmingMessageSource
    {
        private readonly IModel _model;

        public ConfirmingGetter(IModel model)
            :base(model)
        {
            _model = model;
        }

        public string QueueName { get; set; }

        public override bool TryGetNextMessage(out DeliveredRabbitMessage message)
        {
            message = null;

            BasicGetResult result = _model.BasicGet(QueueName, false);
            if (result == null)
            {
                return false;
            }
            
            message = new DeliveredRabbitMessage
                          {
                              Body = result.Body,
                              DeliveryTag = result.DeliveryTag,
                              Exchange = result.Exchange,
                              Properties = result.BasicProperties,
                              Redelivered = result.Redelivered,
                              RoutingKey = result.RoutingKey,
                          };
            return true;
        }
    }
}
