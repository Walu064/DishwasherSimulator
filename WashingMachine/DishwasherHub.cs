using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dishwasher
{
    public class DishwasherHub : Hub
    {
        DishwasherDevice dishwasherDevice;

        public DishwasherHub(DishwasherDevice device)
        {
            dishwasherDevice = device;
        }

        public async Task TurnOn(int time,int water,int temperature, int whirlspeed)
        {
            await dishwasherDevice.TurnOn(time,water,temperature,whirlspeed);
        }

        public async Task TurnOf()
        {
            await dishwasherDevice.TurnOf();
        }
        public async Task TurnOfWater()
        {
            await dishwasherDevice.TurnOfWater();
        }
        public async Task TurnOfHeating()
        {
            await dishwasherDevice.TurnOfHeating();
        }
        public async Task TurnOfEngine()
        {
            await dishwasherDevice.TurnOfEngine();
        }

        public async Task TimeChange(int time)
        {
            await Clients.All.SendAsync("TimeChange", time);
        }

        public async Task WaterChange(int water)
        {
            await Clients.All.SendAsync("WaterChange", water);
        }

        public async Task TurncockChange(bool valve)
        {
            await Clients.All.SendAsync("TurncockChange", valve);
        }

        public async Task Heat(int temperature)
        {
            await Clients.All.SendAsync("Heat", temperature);
        }

        public async Task Whirling(int whirl)
        {
            await Clients.All.SendAsync("washingIntensity", whirl);
        }
    }
}
