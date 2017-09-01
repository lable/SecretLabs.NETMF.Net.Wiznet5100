using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using SecretLabs.NETMF.Net;
using Microsoft.SPOT.Net.NetworkInformation;

namespace WiznetConsole
{
    public class Program
    {
        public static void Main()
        {
            // write your code here

            Debug.Print("hello");

            /*
            SPI.Configuration spiConfig = new SPI.Configuration(
                ChipSelect_Port: Pins.GPIO_PIN_D4,      // Chip select is digital IO 4.
                ChipSelect_ActiveState: false,          // Chip select is active low.
                ChipSelect_SetupTime: 0,                // Amount of time between selection and the clock starting
                ChipSelect_HoldTime: 0,                 // Amount of time the device must be active after the data has been read.
                Clock_Edge: false,                      // Sample on the falling edge.
                Clock_IdleState: true,                  // Clock is idle when high.
                Clock_RateKHz: 2000,                    // 2MHz clock speed.
                SPI_mod: SPI_Devices.SPI1               // Use SPI1
            );

            SPI spi = new SPI(spiConfig);
             * */

            NetworkChange.NetworkAvailabilityChanged += (sender, e) =>
                {
                    if (e.IsAvailable)
                    {
                        Debug.Print("IsAvailable");
                    }
                    else
                    {
                        Debug.Print("not IsAvailable");
                    }
                };

            OutputPort rst = new OutputPort(Pins.GPIO_PIN_D3, false);
            rst.Write(false);
            Thread.Sleep(1000);
            rst.Write(true);


            //var wiznet = new Wiznet5100(spi, (Cpu.Pin)Pins.GPIO_PIN_D4);
            var wiznet = new Wiznet5100(SPI_Devices.SPI1, (Cpu.Pin)Pins.GPIO_PIN_D4);


            var interfaces = NetworkInterface.GetAllNetworkInterfaces();

            if (interfaces.Length > 0)
            {
                interfaces[0].PhysicalAddress = new byte[] { 0x02, 0x02, 0x02, 0x03, 0x03, 0x03 };

                interfaces[0].EnableStaticIP("192.168.1.15", "255.255.255.0", "192.168.1.1");
                
                Debug.Print(interfaces[0].IPAddress);

            }
            while (true)
            {
                Thread.Sleep(10);
            }
        }

    }
}
