using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Symulator.Core
{
    public class ThermometerClient : IAsyncDisposable
    {
        HubConnection thermometerServer;

        public event EventHandler<int> Heat;

        public ThermometerClient()
        {
            thermometerServer = new HubConnectionBuilder()
                .WithUrl(@"http://host.docker.internal:36002/thermometer")
                .AddMessagePackProtocol()
                .Build();

            thermometerServer.Closed += async (error) =>
            {
                Console.WriteLine("Connection Closed");
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await thermometerServer.StartAsync();
            };

            thermometerServer.On<int>("Heat", (temperature) =>
            {
                
                Heat?.Invoke(this, temperature);
            });

            thermometerServer.StartAsync().Wait();

        }

        public ValueTask DisposeAsync()
        {
            return thermometerServer.DisposeAsync();
        }

        public async Task SimulatedTemperature(int temperature,int temperatureMax)
        {
            await thermometerServer.SendAsync("SimulatedTemperature", temperature,temperatureMax);
        }
    }
}
