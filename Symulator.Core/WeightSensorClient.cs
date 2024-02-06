using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Symulator.Core
{
    public class WeightSensorClient : IAsyncDisposable
    {
        HubConnection weightSensorServer;

        public event EventHandler<int> WeightSens;

        public WeightSensorClient()
        {
            weightSensorServer = new HubConnectionBuilder()
                .WithUrl(@"http://localhost:36005/weightSensor")
                .AddMessagePackProtocol()
                .Build();


            weightSensorServer.Closed += async (error) =>
            {
                Console.WriteLine("Connection Closed");
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await weightSensorServer.StartAsync();
            };

            weightSensorServer.On<int>("weightSensors", (weight) =>
            {
                WeightSens?.Invoke(this, weight);
            });


            weightSensorServer.StartAsync().Wait();
        }

        public ValueTask DisposeAsync()
        {
            return weightSensorServer.DisposeAsync();
        }

        public async Task SimulateweightSensor(int weight)
        {
            await weightSensorServer.SendAsync("weightSensors:", weight);
        }

    }
}
