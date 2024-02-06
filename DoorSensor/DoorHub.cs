using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoorSensor
{
    public class DoorHub : Hub
    {
        Door pDevice;

        public DoorHub(Door device)
        {
            pDevice = device;
        }

        public async Task SimulateDoor()
        {
            await pDevice.OpenClose();
            
        }

    }
}
