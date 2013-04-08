using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RaspberryPi_CSharp_Lib
{
    /// <summary>
    ///  Enum that represents a GPIO Pin from the RaspberryPi
    /// </summary>
    public enum Pin : int
    {
        // RPi Version 1
        v1_gpio0 = 0,  ///< Version 1, Pin P1-03
        v1_gpio1 = 1,  ///< Version 1, Pin P1-05
        v1_gpio4 = 4,  ///< Version 1, Pin P1-07
        v1_gpio14 = 14,  ///< Version 1, Pin P1-08, defaults to alt function 0 UART0_TXD
        v1_gpio15 = 15,  ///< Version 1, Pin P1-10, defaults to alt function 0 UART0_RXD
        v1_gpio17 = 17,  ///< Version 1, Pin P1-11
        v1_gpio18 = 18,  ///< Version 1, Pin P1-12
        v1_gpio21 = 21,  ///< Version 1, Pin P1-13
        v1_gpio22 = 22,  ///< Version 1, Pin P1-15
        v1_gpio23 = 23,  ///< Version 1, Pin P1-16
        v1_gpio24 = 24,  ///< Version 1, Pin P1-18
        v1_gpio10 = 10,  ///< Version 1, Pin P1-19, MOSI when SPI0 in use
        v1_gpio9 = 9,  ///< Version 1, Pin P1-21, MISO when SPI0 in use
        v1_gpio25 = 25,  ///< Version 1, Pin P1-22
        v1_gpio11 = 11,  ///< Version 1, Pin P1-23, CLK when SPI0 in use
        v1_gpio8 = 8,  ///< Version 1, Pin P1-24, CE0 when SPI0 in use
        v1_gpio7 = 7,  ///< Version 1, Pin P1-26, CE1 when SPI0 in use

        // RPi Version 2
        v2_gpio2 = 2,  ///< Version 2, Pin P1-03
        v2_gpio3 = 3,  ///< Version 2, Pin P1-05
        v2_gpio4 = 4,  ///< Version 2, Pin P1-07
        v2_gpio14 = 14,  ///< Version 2, Pin P1-08, defaults to alt function 0 UART0_TXD
        v2_gpio15 = 15,  ///< Version 2, Pin P1-10, defaults to alt function 0 UART0_RXD
        v2_gpio17 = 17,  ///< Version 2, Pin P1-11
        v2_gpio18 = 18,  ///< Version 2, Pin P1-12
        v2_gpio27 = 27,  ///< Version 2, Pin P1-13
        v2_gpio22 = 22,  ///< Version 2, Pin P1-15
        v2_gpio23 = 23,  ///< Version 2, Pin P1-16
        v2_gpio24 = 24,  ///< Version 2, Pin P1-18
        v2_gpio10 = 10,  ///< Version 2, Pin P1-19, MOSI when SPI0 in use
        v2_gpio9 = 9,  ///< Version 2, Pin P1-21, MISO when SPI0 in use
        v2_gpio25 = 25,  ///< Version 2, Pin P1-22
        v2_gpio11 = 11,  ///< Version 2, Pin P1-23, CLK when SPI0 in use
        v2_gpio8 = 8,  ///< Version 2, Pin P1-24, CE0 when SPI0 in use
        v2_gpio7 = 7,  ///< Version 2, Pin P1-26, CE1 when SPI0 in use

        // RPi Version 2, new plug P5
        v2_gpio28 = 28,  ///< Version 2, Pin P5-03
        v2_gpio29 = 29,  ///< Version 2, Pin P5-04
        v2_gpio30 = 30,  ///< Version 2, Pin P5-05
        v2_gpio31 = 31,  ///< Version 2, Pin P5-06
    };

    /// <summary>
    ///  Enum that represents the Direction of the Pin
    /// </summary>
    public enum Direction
    {
        IN, OUT
    };

    /// <summary>
    ///  Enum that represents a State that is the Input/Output value by a Pin
    /// </summary>
    public enum State : int
    {
        LOW = 0,
        HIGH = 1
    };

    /// <summary>
    ///  Internal class used by it's childs. It represents all port types.
    /// </summary>
    abstract class IOPorts
    {
        private static List<Pin> _activePins = new List<Pin>();
        protected static String GPIO_PATH = "/sys/class/gpio/";

        protected IOPorts() { }

        /// <summary>
        ///  Close and Release all the pins used by this class and it's childs
        /// </summary>
        public static void CleanAllPins()
        {
            foreach(Pin p in _activePins){
                File.WriteAllText(GPIO_PATH + "unexport", ((int) p).ToString());
            }
        }

        /// <summary>
        ///  Check if the Pin is exported or not
        /// </summary>
        /// <param name="pin">
        ///   A <see cref="Pin"/> type representing a pin.
        /// </param>
        protected bool _isExported(Pin pin)
        {
            if (_activePins.Contains(pin))
            {
                return true;
            }
            else {
                List<String> l = Directory.GetDirectories(GPIO_PATH).ToList();
                if(l.Contains(GPIO_PATH+pin.ToString().Split('_')[1])) {
                   
                    return true;
                }
                else{

                    return false;
                }
            }
        }

        /// <summary>
        ///  Activate a GPIO in the RaspberryPi
        /// </summary>
        /// <param name="pin">
        ///   A <see cref="Pin"/> type representing a pin.
        /// </param>
        protected bool _Export(Pin pin)
        {
            if (!this._isExported(pin))
            {
                File.WriteAllText(GPIO_PATH + "export", ((int)pin).ToString());
                _activePins.Add(pin);
                return true;
            }
            return false;
        }

        /// <summary>
        ///  Deactivate a GPIO in the RaspberryPi
        /// </summary>
        /// <param name="pin">
        ///   A <see cref="Pin"/> type representing a pin.
        /// </param>
        protected void _Unexport(Pin pin)
        {
            if (_activePins.Contains(pin))
            {
                File.WriteAllText(GPIO_PATH + "unexport", ((int)pin).ToString());
                _activePins.Remove(pin);
            }
        }
    }
}
