using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Symulator.Core;

namespace ValveSensor
{
    public class Valve : IAsyncDisposable
    {
        bool open;

        ValveSensorClient valve;
        public Valve()
        {
            open = false;
            valve = new ValveSensorClient();
        }

        public ValueTask DisposeAsync()
        {
            return valve.DisposeAsync();
        }

        public void SimulateValve2(bool change)
        {
            

            open = !open;
            if (open == false)
            {
                Console.WriteLine("Turncock Closed!\n");
            }
            else
            {
                Console.WriteLine("Turncock Open!\n");
            }
        }
    }
}
