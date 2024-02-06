using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Symulator.Core
{

    public class WashingMachineClient : IAsyncDisposable
    {
        HubConnection washingMachineServer;
        public WashingMachineClient()
        {
            washingMachineServer = new HubConnectionBuilder()
                .WithUrl(@"http://host.docker.internal:36000/dishwasher")
                .AddMessagePackProtocol()
                .Build();


            washingMachineServer.Closed += async (error) =>
            {
                Console.WriteLine("Connection closed");
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await washingMachineServer.StartAsync();
            };

            washingMachineServer.StartAsync().Wait();
        }

        public ValueTask DisposeAsync()
        {
            return washingMachineServer.DisposeAsync();
        }

        public async Task TurnOn(int time, int water, int temperature, int whirlspeed)
        {
            await washingMachineServer.SendAsync("TurnOn", time, water, temperature, whirlspeed);
        }

        public async Task TurnOf()
        {
            await washingMachineServer.SendAsync("TurnOf");
        }
        public async Task TurnOfWater()
        {
            await washingMachineServer.SendAsync("TurnOfWater");
        }
        public async Task TurnOfHeating()
        {
            await washingMachineServer.SendAsync("TurnOfHeating");
        }

        public async Task TurnOfEngine()
        {
            await washingMachineServer.SendAsync("TurnOfEngine");
        }
    }
}
