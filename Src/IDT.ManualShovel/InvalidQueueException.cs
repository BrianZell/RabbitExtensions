using System;
using System.Runtime.Serialization;

namespace IDT.ManualShovel
{
    [Serializable]
    public class InvalidQueueException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public InvalidQueueException()
        {
        }

        public InvalidQueueException(string message)
            : base(message)
        {
        }

        public InvalidQueueException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected InvalidQueueException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}