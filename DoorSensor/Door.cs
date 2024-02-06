using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Symulator.Core;

namespace DoorSensor
{
    public class Door
    {

        bool open;

        public Door()
        {
            open = false;

            Console.WriteLine("Door: Closed");
        }

        public async Task OpenClose()
        {
            open = !open;
            if (open == false)
            {
                Console.WriteLine("Door Open!");
            }
            else
            {
                Console.WriteLine("Door Closed!");
            }
        }
    }
}