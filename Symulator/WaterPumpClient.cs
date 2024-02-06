using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Symulator.Core
{
    public class WaterPumpClient : IAsyncDisposable
    {
        HubConnection waterPumpServer;

        public event EventHandler<int> WaterChange;

        public WaterPumpClient()
        {
            waterPumpServer = new HubConnectionBuilder()
                .WithUrl(@"http://host.docker.internal:36004/waterPump")
                .AddMessagePackProtocol()
                .Build();


            waterPumpServer.Closed += async (error) =>
            {
                Console.WriteLine("Connection Closed");
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await waterPumpServer.StartAsync();
            };

            waterPumpServer.On<int>("WaterChange", (waterLevel) =>
            {
                WaterChange?.Invoke(this, waterLevel);
            });


            waterPumpServer.StartAsync().Wait();
        }

        public ValueTask DisposeAsync()
        {
            return waterPumpServer.DisposeAsync();
        }

        public async Task SimulateWaterPump(int waterLevel)
        {
            await waterPumpServer.SendAsync("SimulateWaterPump", waterLevel);
        }
        public async Task SimulateWaterPumpPour(int waterLevel)
        {
            await waterPumpServer.SendAsync("SimulateWaterPumpPour", waterLevel);
        }

    }
}
