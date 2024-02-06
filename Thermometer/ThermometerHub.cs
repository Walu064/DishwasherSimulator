using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThermometerSensor
{
    public class ThermometerHub : Hub
    {
        Thermometer pDevice;

        public ThermometerHub(Thermometer device)
        {
            pDevice = device;
        }

        public async Task SimulatedTemperature(int temperature,int temperatureMax)
        {
            pDevice.ThermometerTemperature(temperature,temperatureMax);
            _ = Clients.All.SendAsync("Heat", temperature);
            await Task.CompletedTask;
        }

    }
}
