using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaspberryPi_CSharp_Lib
{
    /// <summary>Exception indicating that an error has occurred while activating a Pin</summary>
    [Serializable()]
    class ActivatePinException : System.Exception
    {
        public ActivatePinException(string message) : base(message) { }

        protected ActivatePinException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) { }

        public override string ToString()
        {
            return "\n"+this.Message + "\n\n" + this.StackTrace;
        }
    }
}
