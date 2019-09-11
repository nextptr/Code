using System;
using System.Runtime.Serialization;

namespace Exceptions
{
    [Serializable]
    public class AcsReadWriteException : Exception
    {
        public readonly string _readwriteInfo = "";
        public AcsReadWriteException() { }
        public AcsReadWriteException(string message) : base(message) { }
        public AcsReadWriteException(string message, Exception inner) : base(message, inner) { }
        protected AcsReadWriteException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        public AcsReadWriteException(string message, string readwriteInfo) : base(message)
        {
            _readwriteInfo = readwriteInfo;
        }
        public AcsReadWriteException(string message, string readwriteInfo, Exception inner) : base(message, inner)
        {
            _readwriteInfo = readwriteInfo;
        }
        public override string Message
        {
            get
            {
                return base.Message + " " + _readwriteInfo;
            }
        }
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Args", _readwriteInfo);
            base.GetObjectData(info, context);
        }
    }
}
