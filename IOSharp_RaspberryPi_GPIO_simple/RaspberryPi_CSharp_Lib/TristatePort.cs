using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RaspberryPi_CSharp_Lib
{
    /// <summary>This class is used to set up an TristatePort. Used to hotswap Pins between Input and Output</summary>
    class TristatePort : IOPorts
    {
        private Pin _p;
        private Direction _direction;

        /// <summary>
        ///  Construct a new TristatePort object
        /// </summary>
        /// <param name="pin">
        ///   A <see cref="Pin"/> type representing a pin
        /// </param>
        /// <exception cref="ActivatePinException"></exception>
        public TristatePort(Pin pin)
        {
            this._p = pin;
            bool export = this._Export(_p);
            if(!export)
                throw new ActivatePinException("Pin is working, stop first!");
        }

        /// <summary>
        ///  Set the direction of the Pin (Input or Output)
        /// </summary>
        /// <param name="d">
        ///   A <see cref="Direction"/> type representing direction of the Pin
        /// </param>
        public void SetDirection(Direction d)
        {
            this._direction = d;
            File.WriteAllText(GPIO_PATH + _p.ToString().Split('_')[1] + "/direction", d.ToString().ToLower());
        }

        /// <summary>
        ///  Read the value incomming from the Pin
        /// </summary>
        /// <exception cref="DirectionException"></exception>
        public State ReadValue()
        {
            if (_direction == Direction.IN)
            {
                String value = File.ReadAllText(GPIO_PATH + _p.ToString().Split('_')[1] + "/value");
                if (value == ((int)State.HIGH).ToString().ToLower())
                    return State.HIGH;
                else
                    return State.LOW;
            }
            throw new DirectionException("Reading Input from an Output Port. BAD!");
        }

        /// <summary>
        ///  Change the output value and send it through the port
        /// </summary>
        /// <param name="state">
        ///   A <see cref="State"/> type representing the output State.HIGH or State.LOW
        /// </param>
        /// <exception cref="DirectionException"></exception>
        public void ChangeOutputState(State state)
        {
            if (_direction == Direction.OUT)
            {
                File.WriteAllText(GPIO_PATH + _p.ToString().Split('_')[1] + "/value", ((int)state).ToString().ToLower());
            }
            throw new DirectionException("Setting output value to an Input Port. BAD!");
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
