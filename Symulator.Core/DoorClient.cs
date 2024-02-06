using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Symulator.Core
{
    public class DoorClient : IAsyncDisposable
    {
        HubConnection doorServer;

        public event EventHandler<bool> ChangeDoor;

        public DoorClient()
        {
            doorServer = new HubConnectionBuilder()
                .WithUrl(@"http://localhost:36001/door")
                .AddMessagePackProtocol()
                .Build();


            doorServer.Closed += async (error) =>
            {
                Console.WriteLine("Connection Closed");
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await doorServer.StartAsync();
            };

            doorServer.On<bool>("Doors ", (open) =>
            {
                ChangeDoor?.Invoke(this, open);
            });


            doorServer.StartAsync().Wait();
        }

        public ValueTask DisposeAsync()
        {
            return doorServer.DisposeAsync();
        }

        public async Task SimulateDoor(bool open)
        {
            await doorServer.SendAsync("Doors:", open);
        }

    }
}
