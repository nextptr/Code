using System;
using System.Runtime.Serialization;

namespace Exceptions
{
    [Serializable]
    public class AcsMotionException : Exception
    {
        public readonly string _motionInfo = "";
        public AcsMotionException() { }
        public AcsMotionException(string message) : base(message) { }
        public AcsMotionException(string message, Exception inner) : base(message, inner) { }
        protected AcsMotionException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        public AcsMotionException(string message, string motionInfo) : base(message)
        {
            _motionInfo = motionInfo;
        }
        public AcsMotionException(string message, string motionInfo, Exception inner) : base(message, inner)
        {
            _motionInfo = motionInfo;
        }
        public override string Message
        {
            get
            {
                return base.Message + " " + _motionInfo;
            }
        }
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Args", _motionInfo);
            base.GetObjectData(info, context);
        }
    }
}
