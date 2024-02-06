using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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
                .WithUrl(@"http://localhost:36000/washingMachine")
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

        public async Task TurnOn()
        {
            await washingMachineServer.SendAsync("On");
        }

        public async Task TurnOf()
        {
            await washingMachineServer.SendAsync("Of");
        }
    }
}
