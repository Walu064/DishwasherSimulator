using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Symulator.Core;

namespace ThermometerSensor
{
    public class Thermometer : IAsyncDisposable
    {
        int temperature;

        int temperatureMax;

        WashingMachineClient washingMachine;
        public Thermometer()
        {
            washingMachine = new WashingMachineClient();

            Console.WriteLine("Thermometer: KONSTRUKTOR");
        }

        public ValueTask DisposeAsync()
        {
            return washingMachine.DisposeAsync();
        }

        public void ThermometerTemperature(int temperature,int temperatureMax)
        {
            this.temperatureMax = temperatureMax;
            this.temperature = temperature;
            Console.WriteLine("Thermometer: Temperature = {0} / {1}", temperature, temperatureMax);

            if (!(temperature < temperatureMax))
            {
                _ = washingMachine.TurnOfHeating();
                System.Console.WriteLine("Thermometer");
            }
        }
    }
}
