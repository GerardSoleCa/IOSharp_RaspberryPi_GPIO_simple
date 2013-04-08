IOSharp RaspberryPi GPIO simple
===============================
This library is the first proof of concept of my final degree project. It's intended to be a simple library to control easly de GPIOs of the RaspberryPi from C# using Mono.

## Currently implemented:
* __IOPort:__ Manages the GPIO pins 
* __Input Port:__ Use a GPIO pin as an Input to read an incomming value
* __Output Port:__ Use a GPIO pin as an Output and send HIGH or LOW values
* __Tristate Port:__ Swap easly between an Input or an Output port

## Examples:

#### Setup an Output Port
Example on using an OutputPort
```CSharp
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
```
#### Setup a Tristate Port
Example on using a TristatetPort
```CSharp
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
```

## Test this library:
Now the most easy way to test this lib is compile this project in Visual Studio (minimum 2012) and copy the generated binary (EXE) to the RaspberryPi.
Then execute the program using mono like this: `mono RaspberryPi_CSharp_Lib.exe`.

## Reference links:
* [tfc.gsole.cat] (http://tfc.gsole.cat)
* [AlterAid]      (http://eng.alteraid.com)
* [EETAC]         (http://eetac.upc.edu)
* [RaspberryPi]   (http://raspberrypi.org)
