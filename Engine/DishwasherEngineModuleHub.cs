using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DishwasherEngineModule
{
    public class DishwasherEngineModuleHub : Hub
    {
        DishwasherEngineModuleDevice dishwasherEngineModuleDevice;

        public DishwasherEngineModuleHub(DishwasherEngineModuleDevice device)
        {
            dishwasherEngineModuleDevice = device;
        }

        public async Task SimulateEngine(int washingIntensity)
        {
            await dishwasherEngineModuleDevice.Wash(washingIntensity);
            _ = Clients.All.SendAsync("washingIntensity", washingIntensity);
            await Task.CompletedTask;

        }

    }
}
