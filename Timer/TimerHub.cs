using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timer
{
    public class TimerHub : Hub
    {
        TimerDevice pDevice;

        public TimerHub(TimerDevice device)
        {
            pDevice = device;
        }

        public async Task SimulatedTime(int time)
        {
            pDevice.SimulatedTime2(time);
            _ = Clients.All.SendAsync("TimeChange", time);
            await Task.CompletedTask;
        }

    }
}
