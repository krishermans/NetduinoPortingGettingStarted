using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace love_o_meter
{
    public class Program
    {
        private SecretLabs.NETMF.Hardware.AnalogInput tempSensor;
        private OutputPort redLed, yellowLed, blueLed;
        private const float baselineTemp = 18.5f;
        
        public static void Main()
        {
            var prog = new Program();
            prog.SetUp();

            while (true)
            { 
                prog.Loop();
            }

        }

        public void SetUp()
        {
            tempSensor = new SecretLabs.NETMF.Hardware.AnalogInput(Pins.GPIO_PIN_A0);
            tempSensor.SetRange(0, 1023); // This is the default anyway

            redLed = new OutputPort(Pins.GPIO_PIN_D2, false);
            yellowLed = new OutputPort(Pins.GPIO_PIN_D3, false);
            blueLed = new OutputPort(Pins.GPIO_PIN_D4, false);
        }

        public void Loop()
        {
            int sensorVal = tempSensor.Read();
            Debug.Print("Sensor value: " + sensorVal );

            float voltage = (sensorVal / 1024.0f) * 3.3f;
            Debug.Print("Volts: " + voltage);

            float temperature = (voltage - .5f) * 100;
            Debug.Print("Temp (Celsius): " + temperature + "\n");

            if (temperature < baselineTemp)
            {
                redLed.Write(false);
                yellowLed.Write(false);
                blueLed.Write(false);
            }
            else if (temperature >= baselineTemp + 2 && temperature < baselineTemp + 4)
            {
                redLed.Write(true);
                yellowLed.Write(false);
                blueLed.Write(false);
            }
            else if (temperature >= baselineTemp + 4 && temperature < baselineTemp + 6)
            {
                redLed.Write(true);
                yellowLed.Write(true);
                blueLed.Write(false);
            }
            else if (temperature >= baselineTemp + 6)
            {
                redLed.Write(true);
                yellowLed.Write(true);
                blueLed.Write(true);
            }

            Thread.Sleep(500);
        }
    }
}
