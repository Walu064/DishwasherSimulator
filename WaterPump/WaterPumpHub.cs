using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaterPump
{
    public class WaterPumpHub : Hub
    {
        WaterPumpDevice pDevice;

        public WaterPumpHub(WaterPumpDevice device)
        {
            pDevice = device;
        }

        public async Task SimulateWaterPump(int waterLevel)
        {
            pDevice.WaterPumpOn(waterLevel);
            _ = Clients.All.SendAsync("WaterChange",waterLevel);
            await Task.CompletedTask;
        }
        public async Task SimulateWaterPumpPour(int waterLevel)
        {
            pDevice.WaterPumpPour(waterLevel);
            _ = Clients.All.SendAsync("WaterChange", waterLevel);
            await Task.CompletedTask;
        }

    }
}
