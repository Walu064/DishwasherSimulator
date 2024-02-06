using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ValveSensor
{
    public class ValveHub : Hub
    {
        Valve pDevice;

        public ValveHub(Valve device)
        {
            pDevice = device;
        }

        public async Task SimulateValve(bool open)
        {
            pDevice.SimulateValve2(open);
            _ = Clients.All.SendAsync("ValveChange", open);
            await Task.CompletedTask;
        }

    }
}
