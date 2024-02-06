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

        public event EventHandler<bool> ChangewaterPump;

        public WaterPumpClient()
        {
            waterPumpServer = new HubConnectionBuilder()
                .WithUrl(@"http://localhost:36004/waterPump")
                .AddMessagePackProtocol()
                .Build();


            waterPumpServer.Closed += async (error) =>
            {
                Console.WriteLine("Connection Closed");
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await waterPumpServer.StartAsync();
            };

            waterPumpServer.On<bool>("waterPump", (open) =>
            {
                ChangewaterPump?.Invoke(this, open);
            });


            waterPumpServer.StartAsync().Wait();
        }

        public ValueTask DisposeAsync()
        {
            return waterPumpServer.DisposeAsync();
        }

        public async Task SimulatewaterPump(bool open)
        {
            await waterPumpServer.SendAsync("waterPump:", open);
        }

    }
}
