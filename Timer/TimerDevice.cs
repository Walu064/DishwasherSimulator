using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Symulator.Core;

namespace Timer
{
    public class TimerDevice : IAsyncDisposable
    {
        WashingMachineClient washingMachineClient;
        int tTime;
        public TimerDevice()
        {
            washingMachineClient = new WashingMachineClient();

        }

        public ValueTask DisposeAsync()
        {
            return washingMachineClient.DisposeAsync();
        }
        public void SimulatedTime2(int time)
        {
            tTime = time;
            Console.WriteLine("Time left: {0} ", tTime);

            if (tTime<=0)
            {
                _=washingMachineClient.TurnOf();
                System.Console.WriteLine("timer: turn of");
            }
        }
    }
}
