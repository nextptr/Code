using System;
using System.Runtime.Serialization;

namespace Exceptions
{
    [Serializable]
    public class AcsConnectException : Exception
    {
        public readonly string _connectInfo = "";
        public AcsConnectException() { }
        public AcsConnectException(string message) : base(message) { }
        public AcsConnectException(string message, Exception inner) : base(message, inner) { }
        protected AcsConnectException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        public AcsConnectException(string message, string connectInfo) : base(message)
        {
            _connectInfo = connectInfo;
        }
        public AcsConnectException(string message, string connectInfo, Exception inner) : base(message, inner)
        {
            _connectInfo = connectInfo;
        }
        public override string Message
        {
            get
            {
                return base.Message + " " + _connectInfo;
            }
        }
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Args", _connectInfo);
            base.GetObjectData(info, context);
        }
    }
}
