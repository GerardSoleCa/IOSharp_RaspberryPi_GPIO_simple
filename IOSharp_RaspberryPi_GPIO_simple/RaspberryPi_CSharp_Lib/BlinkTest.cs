using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace RaspberryPi_CSharp_Lib
{
    class BlinkTest
    {
        static void Main(string[] args)
        {
            // Setup a Pin as OutputPort
            try
            {
                OutputPort o = new OutputPort(Pin.v1_gpio17, State.HIGH);

                // Blink 5 Times a LED
                for (int i = 0; i < 5; i++)
                {
                    o.ChangeOutputState(State.LOW);
                    Thread.Sleep(500);
                    o.ChangeOutputState(State.HIGH);
                    Thread.Sleep(500);
                }

                // Close the Port
                o.ClosePort();
            }
            catch (ActivatePinException e)
            {
                Console.WriteLine(e.Message);
            }

            // Now setup a TriState Port
            try
            {
                TristatePort t = new TristatePort(Pin.v1_gpio17);
                t.SetDirection(Direction.IN);

                // Read the value and print
                Console.WriteLine(t.ReadValue());

                // Change the direction and try to read again
                t.SetDirection(Direction.OUT);
                Console.WriteLine(t.ReadValue());
                t.ClosePort();
            }
            catch (ActivatePinException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (DirectionException e)
            {
                // Print the error
                Console.WriteLine(e.Message);
            }

            // Clean all Pins
            IOPorts.CleanAllPins();
        }
    }
}
