using RabbitMQ.Client;

namespace IDT.RabbitMQ.Extensions
{
    public class QualityOfService
    {
        private readonly uint _prefetchSize;
        private readonly ushort _prefetchCount;

        public QualityOfService(uint prefetchSize, ushort prefetchCount)
        {
            _prefetchSize = prefetchSize;
            _prefetchCount = prefetchCount;
        }

        public void Set(IModel model)
        {
            model.BasicQos(_prefetchSize,_prefetchCount,false);
        }
    }
}
