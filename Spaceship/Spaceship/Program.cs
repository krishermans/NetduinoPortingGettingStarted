using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace Spaceship
{
    public class Program
    {
        private static OutputPort greenLed;
        private static OutputPort red1Led, red2Led;
        private static InputPort button;

        public static void SetUp()
        {
            greenLed = new OutputPort(Pins.GPIO_PIN_D3, true);
            red1Led = new OutputPort(Pins.GPIO_PIN_D4, false);
            red2Led = new OutputPort(Pins.GPIO_PIN_D5, false);

            button = new InputPort(Pins.GPIO_PIN_D2, false, Port.ResistorMode.Disabled);
        }
        
        public static void Main()
        {
            SetUp();

            while (true)
            {
                bool buttonState = button.Read();
                if (buttonState == false)
                {
                    greenLed.Write(true);
                    red1Led.Write(false);
                    red2Led.Write(false);
                }
                else
                {
                    greenLed.Write(false);
                    red1Led.Write(false);
                    red2Led.Write(true);
                    Thread.Sleep(250);

                    red1Led.Write(true);
                    red2Led.Write(false);
                    Thread.Sleep(250);
                }
            }
        }

    }
}
