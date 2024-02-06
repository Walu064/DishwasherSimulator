using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Symulator.Core;

namespace WaterPump
{
    public class WaterPumpDevice : IAsyncDisposable
    {
        int wWaterLevel;
        WashingMachineClient washingMachineClient;
        public WaterPumpDevice()
        {
            washingMachineClient = new WashingMachineClient();
        }

        public ValueTask DisposeAsync()
        {
            return washingMachineClient.DisposeAsync();
        }

        public void WaterPumpOn(int waterLevel)
        {

            wWaterLevel = waterLevel;
            Console.WriteLine("Water Level: {0} ", wWaterLevel);

            if (wWaterLevel >=10)
            {
                _ = washingMachineClient.TurnOfWater();
                System.Console.WriteLine("WaterPump: turn of");
            }
        }
        public void WaterPumpPour(int waterLevel)
        {

            wWaterLevel = waterLevel;
            Console.WriteLine("Water Level: {0} ", wWaterLevel);

            if (wWaterLevel <= 0)
            {
                _ = washingMachineClient.TurnOfWater();
                System.Console.WriteLine("WaterPump: turn of");
            }
        }
    }
}
