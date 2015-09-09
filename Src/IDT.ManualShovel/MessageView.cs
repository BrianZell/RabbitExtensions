using IDT.RabbitMQ.Extensions;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace IDT.ManualShovel
{
    public class MessageView : INotifyPropertyChanged
    {
        private readonly DeliveredRabbitMessage _message;
        private readonly Lazy<Dictionary<string, object>> _xDeathHeader;
        private readonly Lazy<string> _dlQueue;
        private readonly Lazy<string> _dlReason;
        private readonly Lazy<string> _dlExchange;
        private readonly Lazy<DateTime?> _dlTime;
        private readonly Lazy<string> _dlRoutingKeys;
        private bool _selected;


        public MessageView(DeliveredRabbitMessage message)
        {
            _message = message;
            _xDeathHeader = new Lazy<Dictionary<string, object>>(() => GetHeaderDictionary("x-death"));
            _dlReason = new Lazy<string>(() => GetByteArrayProperty(_xDeathHeader.Value, "reason"));
            _dlQueue = new Lazy<string>(() => GetByteArrayProperty(_xDeathHeader.Value, "queue"));
            _dlTime = new Lazy<DateTime?>(() => GetTimestampArrayProperty(_xDeathHeader.Value,"time"));
            _dlExchange = new Lazy<string>(() => GetByteArrayProperty(_xDeathHeader.Value, "exchange"));
            _dlRoutingKeys = new Lazy<string>(() => FlattenListProperty(_xDeathHeader.Value, "routing-keys"));
        }

        public bool Selected 
        {
            get { return _selected; }
            set
            {
                if (_selected != value)
                {
                    _selected = value;    
                    OnPropertyChanged("Selected");
                }
            } 
        }

        public string Exchange
        {
            get { return _message.Exchange; }
        }

        public string RoutingKey
        {
            get { return _message.RoutingKey; }
        }

        public DateTime Timestamp
        {
            get { return _message.Properties.Timestamp.ToDateTime(); }
        }

        public string MessageId
        {
            get { return _message.Properties.MessageId; }
        }

        public string Type
        {
            get { return _message.Properties.Type; }
        }

        public string Expiration
        {
            get { return _message.Properties.Expiration; }
        }

        public string ReplyTo
        {
            get { return _message.Properties.ReplyTo; }
        }

        public string DLQueue
        {
            get { return _dlQueue.Value; }
        }

        public string DLReason
        {
            get { return _dlReason.Value; }
        }

        public DateTime? DLTime
        {
            get { return _dlTime.Value; }
        }

        public string DLExchange
        {
            get { return _dlExchange.Value; }
        }

        public string DLRoutingKeys
        {
            get { return _dlRoutingKeys.Value; }
        }

        public DeliveredRabbitMessage ProvideOriginalMessage()
        {
            return _message;
        }

        public string DisplayMessage()
        {
            return _message.ToString();
        }

        private string GetByteArrayProperty(IDictionary<string,object> dict, string key)
        {
            if (!dict.ContainsKey(key))
            {
                return string.Empty;
            }

            var bArray = dict[key] as byte[];
            if (bArray == null)
            {
                return string.Empty;
            }

            return Encoding.UTF8.GetString(bArray);
        }

        private DateTime? GetTimestampArrayProperty(IDictionary<string, object> dict, string key)
        {
            if (!dict.ContainsKey(key))
            {
                return null;
            }
            
            if (!(dict[key] is AmqpTimestamp))
            {
                return null;
            }

            var timestamp = (AmqpTimestamp) dict[key];
            return timestamp.ToDateTime();
        }

        private string FlattenListProperty(IDictionary<string, object> dict, string key)
        {
            if (!dict.ContainsKey(key))
            {
                return string.Empty;
            }

            var list = dict[key] as List<object>;
            if (list == null)
            {
                return string.Empty;
            }

            var resultlist = list.Select(x => x is byte[] ? Encoding.UTF8.GetString((byte[]) x) : x.ToString());
            return string.Join(";", resultlist);
        }

        private Dictionary<string, object> GetHeaderDictionary(string propName)
        {
            var headers = _message.Properties.Headers;
            if (headers == null)
            {
                return new Dictionary<string, object>();
            }

            if (!headers.ContainsKey(propName))
            {
                return new Dictionary<string, object>();
            }

            var list = headers[propName] as List<object>;
            if (list == null)
            {
                return new Dictionary<string, object>();
            }

            var dict = list.First() as Dictionary<string, object>;
            if (dict == null)
            {
                return new Dictionary<string, object>();
            }

            return dict;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
