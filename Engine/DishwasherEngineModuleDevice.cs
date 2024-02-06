using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Symulator.Core;

namespace DishwasherEngineModule
{
    public class DishwasherEngineModuleDevice
    {
        int washingIntensity;
        public DishwasherEngineModuleDevice()
        {
            washingIntensity = 0;
        }

        public async Task Wash(int washingIntensity)
        {
            Console.WriteLine("Washing intensity: {0}",washingIntensity);
        }
    }
}