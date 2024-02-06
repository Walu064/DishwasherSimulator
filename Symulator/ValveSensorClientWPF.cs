using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Symulator.Core
{
    public class ValveSensorClientWPF : IAsyncDisposable
    {
        HubConnection valveSensorServer;

        public event EventHandler<bool> ValveChange;

        public ValveSensorClientWPF()
        {
            valveSensorServer = new HubConnectionBuilder()
                .WithUrl(@"http://localhost:36003/valve")
                .AddMessagePackProtocol()
                .Build();


            valveSensorServer.Closed += async (error) =>
            {
                Console.WriteLine("Connection Closed");
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await valveSensorServer.StartAsync();
            };

            valveSensorServer.On<bool>("ValveChange", (open) =>
            {
                ValveChange?.Invoke(this, open);
            });


            valveSensorServer.StartAsync().Wait();
        }

        public ValueTask DisposeAsync()
        {
            return valveSensorServer.DisposeAsync();
        }

        public async Task SimulateValve(bool open)
        {
            await valveSensorServer.SendAsync("SimulateValve", open);
        }

    }
}