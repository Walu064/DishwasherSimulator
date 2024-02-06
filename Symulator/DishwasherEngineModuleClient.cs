using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Symulator.Core
{
    public class DishwasherEngineModuleClient : IAsyncDisposable
    {
        HubConnection EngineServer;
        public event EventHandler<int> washingIntensity;

        public DishwasherEngineModuleClient()
        {
            EngineServer = new HubConnectionBuilder()
                .WithUrl(@"http://host.docker.internal:36007/dishwasher_engine_module")
                .AddMessagePackProtocol()
                .Build();


            EngineServer.Closed += async (error) =>
            {
                Console.WriteLine("Connection Closed");
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await EngineServer.StartAsync();
            };
            EngineServer.On<int>("washingIntensity", (whirl) =>
            {

                washingIntensity?.Invoke(this, whirl);
            });

            EngineServer.StartAsync().Wait();
        }

        public ValueTask DisposeAsync()
        {
            return EngineServer.DisposeAsync();
        }

        public async Task SimulateEngine(int whirl)
        {
            await EngineServer.SendAsync("SimulateEngine",whirl);
        }

    }
}
