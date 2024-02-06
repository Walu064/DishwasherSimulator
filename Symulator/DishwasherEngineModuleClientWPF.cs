using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Symulator.Core
{
    public class DishwasherEngineModuleClientWPF : IAsyncDisposable
    {
        HubConnection dishwasherEngineModuleServer;

        public event EventHandler<int> wIntensity;
        public DishwasherEngineModuleClientWPF()
        {
            dishwasherEngineModuleServer = new HubConnectionBuilder()
                .WithUrl(@"http://localhost:36007/dishwasher_engine_module")
                .AddMessagePackProtocol()
                .Build();


            dishwasherEngineModuleServer.Closed += async (error) =>
            {
                Console.WriteLine("Connection Closed");
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await dishwasherEngineModuleServer.StartAsync();
            };
            dishwasherEngineModuleServer.On<int>("washingIntensity", (washingIntensity) =>
            {

                wIntensity?.Invoke(this, washingIntensity);
            });

            dishwasherEngineModuleServer.StartAsync().Wait();
        }

        public ValueTask DisposeAsync()
        {
            return dishwasherEngineModuleServer.DisposeAsync();
        }

        public async Task SimulateEngine(int whirl)
        {
            await dishwasherEngineModuleServer.SendAsync("SimulateEngine",whirl);
        }

    }
}
