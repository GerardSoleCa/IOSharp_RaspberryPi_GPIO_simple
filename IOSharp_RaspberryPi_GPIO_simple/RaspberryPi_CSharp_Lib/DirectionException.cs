using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaspberryPi_CSharp_Lib
{
    /// <summary>Exception thrown when the direction don't corresponds with the Read/Write function</summary>
    [Serializable()]
    class DirectionException : System.Exception
    {
        public DirectionException(string message) : base(message) { }

        protected DirectionException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) { }

        public override string ToString()
        {
            return "\n"+this.Message + "\n\n" + this.StackTrace;
        }
    }
}
