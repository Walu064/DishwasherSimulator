using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Symulator.Core
{
    public class TimerClient : IAsyncDisposable
    {
        HubConnection timerServer;

        public event EventHandler<int> TimeChange;

        public TimerClient()
        {
            timerServer = new HubConnectionBuilder()
                .WithUrl(@"http://host.docker.internal:36006/timer")
                .AddMessagePackProtocol()
                .Build();

            timerServer.Closed += async (error) =>
            {
                Console.WriteLine("Connection closed");
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await timerServer.StartAsync();
            };

            timerServer.On<int>("TimeChange", (time) =>
            {
                TimeChange?.Invoke(this, time);
            });

            timerServer.StartAsync().Wait();
        }

        public ValueTask DisposeAsync()
        {
            return timerServer.DisposeAsync();
        }
        public async Task SimulatedTime(int time)
        {
            await timerServer.SendAsync("SimulatedTime", time);
        }
        
    }
}