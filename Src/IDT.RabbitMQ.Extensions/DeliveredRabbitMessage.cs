using System.Xml.Linq;

namespace IDT.RabbitMQ.Extensions
{
    public class DeliveredRabbitMessage : RabbitMessage
    {
        public ulong DeliveryTag { get; set; }
        public bool Redelivered { get; set; }
        public string Exchange { get; set; }

        public override XElement AsXml()
        {
            var baseXml = base.AsXml();
            baseXml.Add(
                new XElement("deliveryTag",this.DeliveryTag),
                new XElement("redelivered",this.Redelivered),
                new XElement("exchange",this.Exchange));
            return baseXml;
        }
    }
}
