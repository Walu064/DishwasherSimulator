using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Symulator.Core
{
    public class CapacitySensorClient : IAsyncDisposable
    {
        HubConnection capacitySensorServer;

        public event EventHandler<int> ChangeNumberOfDishes;

        public CapacitySensorClient()
        {
            capacitySensorServer = new HubConnectionBuilder()
                .WithUrl(@"http://host.docker.internal:36005/capacity_sensor")
                .AddMessagePackProtocol()
                .Build();


            capacitySensorServer.Closed += async (error) =>
            {
                Console.WriteLine("Connection Closed");
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await capacitySensorServer.StartAsync();
            };

            capacitySensorServer.On<int>("ChangeNumberOfDishes", (weight) =>
            {
                ChangeNumberOfDishes?.Invoke(this, weight);
            });


            capacitySensorServer.StartAsync().Wait();
        }

        public ValueTask DisposeAsync()
        {
            return capacitySensorServer.DisposeAsync();
        }

        public async Task SimulateCapacitySensorTask(int numberOfDishes)
        {
            await capacitySensorServer.SendAsync("SimulateCapacitySensorTask", numberOfDishes);
        }
        public async Task SimulateCapacitySensorOutTask(int numberOfDishes)
        {
            await capacitySensorServer.SendAsync("SimulateCapacitySensorOutTask", numberOfDishes);
        }
    }
}
