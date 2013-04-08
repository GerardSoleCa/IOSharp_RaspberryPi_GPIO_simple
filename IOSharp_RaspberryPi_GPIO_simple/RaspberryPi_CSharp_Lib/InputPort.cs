using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RaspberryPi_CSharp_Lib
{
    /// <summary>This class is used to set up an InputPort. Used to hotswap Pins between Input and Output</summary>
    class InputPort : IOPorts
    {
        private Pin _p;

        /// <summary>
        ///  Construct a new InputPort object
        /// </summary>
        /// <param name="pin">
        ///   A <see cref="Pin"/> type representing a pin
        /// </param>
        /// <exception cref="ActivatePinException"></exception>
        public InputPort(Pin pin)
        {
            this._p = pin;
            bool export = this._Export(_p);
            if (!export)
                throw new ActivatePinException("Pin is working, stop first!");
            File.WriteAllText(GPIO_PATH + _p.ToString().Split('_')[1] + "/direction", Direction.IN.ToString().ToLower());
        }

        /// <summary>
        ///  Read the value incomming from the Pin
        /// </summary>
        public State ReadValue()
        {
           String value =  File.ReadAllText(GPIO_PATH + _p.ToString().Split('_')[1] + "/value");
           if (value == ((int)State.HIGH).ToString().ToLower())
               return State.HIGH;
           else
               return State.LOW;
        }

        /// <summary>
        ///  Close and release the pin used by this port.
        /// </summary>
        public void ClosePort()
        {
            this._Unexport(_p);
        }
    }
}
