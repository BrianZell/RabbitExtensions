using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Framing;

namespace IDT.RabbitMQ.Extensions
{
    public class RabbitMessage
    {
        public RabbitMessage()
        {
            RoutingKey = string.Empty;
            Properties = new BasicProperties();
        }

        public IBasicProperties Properties { get; set; }
        public byte[] Body { get; set; }
        public string RoutingKey { get; set; }

        public override string ToString()
        {
            return AsXml().ToString();
        }

        public virtual XElement AsXml()
        {
            return new XElement("message",
                        new XElement("headers", AddProperties(), ParseObject(this.Properties.Headers)),
                        new XElement("routingKey", RoutingKey),
                        new XElement("body", Encoding.UTF8.GetString(Body)));
        }

        private object AddProperties()
        {
            var elements = new List<XElement>();
            if (Properties.IsAppIdPresent())
            {
                elements.Add(new XElement("appId",Properties.AppId));
            }
            if (Properties.IsClusterIdPresent())
            {
                elements.Add(new XElement("clusterId", Properties.ClusterId));
            }
            if (Properties.IsContentEncodingPresent())
            {
                elements.Add(new XElement("contentEncoding", Properties.ContentEncoding));
            }
            if (Properties.IsContentTypePresent())
            {
                elements.Add(new XElement("contentType", Properties.ContentType));
            }
            if (Properties.IsCorrelationIdPresent())
            {
                elements.Add(new XElement("correlationId", Properties.CorrelationId));
            }
            if (Properties.IsDeliveryModePresent())
            {
                elements.Add(new XElement("deliveryMode", Properties.DeliveryMode));
            }
            if (Properties.IsExpirationPresent())
            {
                elements.Add(new XElement("expiration", Properties.Expiration));
            }
            if (Properties.IsMessageIdPresent())
            {
                elements.Add(new XElement("messageId", Properties.MessageId));
            }
            if (Properties.IsPriorityPresent())
            {
                elements.Add(new XElement("priority", Properties.Priority));
            }
            if (Properties.IsReplyToPresent())
            {
                elements.Add(new XElement("replyTo", Properties.ReplyTo));
            }
            if (Properties.IsTimestampPresent())
            {
                elements.Add(new XElement("timestamp", Properties.Timestamp.ToDateTime()));
            }
            if (Properties.IsTypePresent())
            {
                elements.Add(new XElement("type", Properties.Type));
            }
            if (Properties.IsUserIdPresent())
            {
                elements.Add(new XElement("userId", Properties.UserId));
            }
            
            return elements;
        }

        private object ParseObject(object item)
        {
            if (item is List<object>)
            {
                return ((List<object>)item).Select(x => ParseObject(x));
            }
            else if (item is Dictionary<string, object>)
            {
                return ((Dictionary<string, object>) item).Select(x => new XElement(x.Key, ParseObject(x.Value)));
            }
            else if (item is byte[])
            {
                return Encoding.UTF8.GetString((byte[])item);
            }

            return item;
        }
    }
}