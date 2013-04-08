using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RaspberryPi_CSharp_Lib
{
    /// <summary>
    ///  This class is used to set up an OutputPort.
    /// </summary>
    class OutputPort : IOPorts
    {

        private Pin _p;

        /// <summary>
        ///  Construct a new OutputPort object
        /// </summary>
        /// <param name="pin">
        ///   A <see cref="Pin"/> type representing a pin
        /// </param>
        /// <param name="state">
        ///   A <see cref="State"/> type representing the output State.HIGH or State.LOW
        /// </param>
        /// <exception cref="ActivatePinException"></exception>
        public OutputPort(Pin pin, State state)
        {
            this._p = pin;
            bool export = this._Export(_p);
            if (!export)
                throw new ActivatePinException("Pin is working, stop first!");
            File.WriteAllText(GPIO_PATH + _p.ToString().Split('_')[1] + "/direction", Direction.OUT.ToString().ToLower());
            this.ChangeOutputState(state);

        }

        /// <summary>
        ///  Change the output value and send it through the port
        /// </summary>
        /// <param name="state">
        ///   A <see cref="State"/> type representing the output State.HIGH or State.LOW
        /// </param>
        public void ChangeOutputState(State state)
        {
           File.WriteAllText(GPIO_PATH + _p.ToString().Split('_')[1] + "/value", ((int) state).ToString().ToLower());
        }

        /// <summary>
        ///  Close and release the pin used by this port.
        /// </summary>
        public void ClosePort(){
            this._Unexport(_p);
        }
    }
}
