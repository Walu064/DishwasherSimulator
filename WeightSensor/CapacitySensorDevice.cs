using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Symulator.Core;

namespace CapacitySensor
{
    public class CapacitySensorDevice : IAsyncDisposable
    {
        int dishCounter;

        CapacitySensorClient capacitySensorDevice;
        public CapacitySensorDevice()
        {
            dishCounter = 0;
            capacitySensorDevice = new CapacitySensorClient();
        }

        public ValueTask DisposeAsync()
        {
            return capacitySensorDevice.DisposeAsync();
        }

        public void SimulateCapacitySensor(int numberOfDishes)
        {
            this.dishCounter++;
            Console.WriteLine("Number of dishes:\t{0}",this.dishCounter);
        }
        public void SimulateCapacitySensorOut(int numberOfDishes)
        {
            this.dishCounter = 0;
            Console.WriteLine("Number of dishes:\t{0}", this.dishCounter);
        }
    }
}
