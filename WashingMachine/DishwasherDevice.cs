using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Symulator.Core;

namespace Dishwasher
{
    public class DishwasherDevice : IAsyncDisposable
    {

        bool isRunning,isRunningWater,isRunningHeating;

        TimerClient timerClient;
        WaterPumpClient waterPumpClient;
        ValveSensorClient turncockSensorClient;
        CapacitySensorClient capacitySensorClient;
        ThermometerClient temperatureSensorClient;
        DishwasherEngineModuleClient dishwasherEngineModuleClient;

        int tTemperature;
        int tTime;
        int wWaterLevel;
        int numberOfDishes;
        int temperatureMax;
        
        Task simulate;

        public DishwasherDevice()
        {
            isRunning = false;
            isRunningWater = false;
            isRunningHeating = false;
            timerClient = new TimerClient();
            dishwasherEngineModuleClient = new DishwasherEngineModuleClient();
            capacitySensorClient = new CapacitySensorClient();
            turncockSensorClient = new ValveSensorClient();
            waterPumpClient = new WaterPumpClient();
            temperatureSensorClient = new ThermometerClient();
            simulate = Task.CompletedTask;
        }

        void SimulateDishwasher(int time)
        {
            tTime = time;
            isRunning = true;
            while (isRunning == true)
            {   
                tTime--;
                _ = timerClient.SimulatedTime(tTime);

                System.Console.WriteLine("Washing Time left: {0}", tTime);
                Task.Delay(100).Wait();
            }
            
        }
        void SimulateHeating(int temperature)
        {
            temperatureMax = temperature;
            tTemperature = 20;
            isRunningHeating = true;
            while (isRunningHeating == true)
            {
                tTemperature++;
                _ = temperatureSensorClient.SimulatedTemperature(tTemperature,temperatureMax);

                System.Console.WriteLine("Water Temperature: {0}", tTemperature);
                Task.Delay(100).Wait();
            }

        }
        void FillWater(int water)
        {
            OpenValve();
            wWaterLevel = water;
            isRunningWater = true;
            while (isRunningWater == true)
            {
                wWaterLevel++;
                _ = waterPumpClient.SimulateWaterPump(wWaterLevel);

                System.Console.WriteLine("Water Level: {0}", wWaterLevel);
                Task.Delay(100).Wait();
            }
            CloseValve();
        }
        void PouringWater(int water)
        {
            OpenValve();
            wWaterLevel = water;
            isRunningWater = true;
            _ = TurnOfEngine();
            _ = dishwasherEngineModuleClient.SimulateEngine(0);
            while (isRunningWater == true)
            {
                wWaterLevel--;
                _ = waterPumpClient.SimulateWaterPumpPour(wWaterLevel);

                System.Console.WriteLine("Water Level: {0}", wWaterLevel);
                Task.Delay(100).Wait();
            }
            CloseValve();
        }
        
        void OpenValve()
        {
            _ = turncockSensorClient.SimulateValve(true);
        }
        
        void CloseValve()
        {
            _ = turncockSensorClient.SimulateValve(false);
        }
        
        void StartEngine(int whirlspeed)
        {
            _ = dishwasherEngineModuleClient.SimulateEngine(whirlspeed);
        }
        
        public async Task TurnOn(int time,int wwater,int temperature, int whirlspeed)
        {
            if (numberOfDishes < 10)
            {
                System.Console.WriteLine("Filling Water");
                simulate = Task.Run(() => { FillWater(wwater); });
                await Task.CompletedTask;
                System.Console.WriteLine("Heating Water");
                simulate = Task.Run(() => { SimulateHeating(temperature); });
                await Task.CompletedTask;
                System.Console.WriteLine("Engine started");
                simulate = Task.Run(() => { StartEngine(whirlspeed); });
                await Task.CompletedTask;
                System.Console.WriteLine("Washing Started");
                simulate = Task.Run(() => { SimulateDishwasher(time); });
                await Task.CompletedTask;
            }
            else
            {
                Console.WriteLine("Too much dishes");
            }
            
            
        }

        public async Task TurnOfWater()
        { 
            System.Console.WriteLine("Pouring Ended");
            if (isRunningWater == true)
            {
                isRunningWater = false;
            }
            await Task.CompletedTask;
        }

        public async Task TurnOfHeating()
        {
            System.Console.WriteLine("Heating Ended");
            if (isRunningHeating == true)
            {
                isRunningHeating = false;
            }
            await Task.CompletedTask;
        }

        public async Task TurnOfEngine()
        {
            System.Console.WriteLine("Drying Ended");
            await Task.CompletedTask;
        }

        public async Task TurnOf()
        {
            System.Console.WriteLine("Washing Ended");
            System.Console.WriteLine("Pouring out water started");
            simulate = Task.Run(() => { PouringWater(10); });
            await Task.CompletedTask;
            if ((isRunning == true) || (isRunningWater == true)|| (isRunningHeating == true))
            {
                isRunningHeating = false;
                isRunning = false;
                isRunningWater = false;
                tTime = 0;
                temperatureMax = 0;
                wWaterLevel = 0;
            }
            await Task.CompletedTask;
        }

        public ValueTask DisposeAsync()
        {
            return timerClient.DisposeAsync();
        }
    }
}
