using Microsoft.AspNetCore.SignalR;

namespace CapacitySensor
{
    public class CapacitySensorHub : Hub
    {
        CapacitySensorDevice capacitySensorDevice;

        public CapacitySensorHub(CapacitySensorDevice device)
        {
            capacitySensorDevice = device;
        }

        public async Task SimulateCapacitySensorTask(int numberOfDishes)
        {
            capacitySensorDevice.SimulateCapacitySensor(numberOfDishes);
            _ = Clients.All.SendAsync("ChangeNumberOfDishes", numberOfDishes);
            await Task.CompletedTask;
        }
        public async Task SimulateCapacitySensorOutTask(int numberOfDishes)
        {
            capacitySensorDevice.SimulateCapacitySensorOut(numberOfDishes);
            _ = Clients.All.SendAsync("ChangeNumberOfDishes", numberOfDishes);
            await Task.CompletedTask;
        }
    }
}
