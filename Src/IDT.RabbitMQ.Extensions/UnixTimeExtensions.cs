using System;
using RabbitMQ.Client;

namespace IDT.RabbitMQ.Extensions
{
    public static class UnixTimeExtensions
    {
        public static AmqpTimestamp ToAmqpTimestamp(this DateTime value)
        {
            var unixTime = (long)Math.Truncate((value.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds);
            return new AmqpTimestamp(unixTime);
        }

        public static DateTime ToDateTime(this AmqpTimestamp value)
        {
            var date = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return date.AddSeconds(value.UnixTime).ToLocalTime();
        }    
    }
}