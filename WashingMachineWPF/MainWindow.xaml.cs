using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Symulator.Core;
using static System.Net.Mime.MediaTypeNames;

namespace WashingMachineWPF
{
    public partial class MainWindow : Window
    {

        TimerClientWPF timer;
        WaterPumpClientWPF waterPump;
        DoorClientWPF door;

        int temperature = 30;
        int time = 60;
        int whirlSpeed = 600;
        int water = 0;
        int laundry,weightL;
        string washProgram = "everyday";
        
        WashingMachineClientWPF washingMachine;
        ThermometerClientWPF thermometerClient;
        DishwasherEngineModuleClientWPF EngineClient;
        CapacitySensorClientWPF weightSensorClient;
        public MainWindow()
        {
            Task.Delay(10000).Wait();
            InitializeComponent();
            SetDisplayText("Program:\tCodzienny\nTemperatura:\t30 \u00B0C \nCzas:\t\t60 min\nIntensywność:\t600");
            buttonStart.IsEnabled = false;
            buttonStop.IsEnabled = false;
            laundry = 0;
            weightL = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                EngineClient = new DishwasherEngineModuleClientWPF();
                thermometerClient = new ThermometerClientWPF();
                thermometerClient.Heat += TemperatureDegrees;
                weightSensorClient = new CapacitySensorClientWPF();
                weightSensorClient.ChangeLaundry += Laundry;
                waterPump = new WaterPumpClientWPF();
                waterPump.WaterChange += WaterPumpLevel;
                door = new DoorClientWPF();
                washingMachine = new WashingMachineClientWPF();
                timer = new TimerClientWPF();
                timer.TimeChange += TimerTime;
                
                
            });
        }
        private void SetDisplayText(string text)
        {
            displayTextBox.Text = text;
        }
        private void ChangeWashProgramText()
        {
            if (washProgram == "everyday")
            {
                displayTextBox.Text = ("Program:\tIntensywny\nTemperatura:\t90 \u00B0C \nCzas:\t\t120 min\nIntensywność:\t1000");

                temperature = 90;
                time = 120;
                whirlSpeed = 1000;
                washProgram = "intense";
            }
            else if (washProgram == "intense")
            {
                displayTextBox.Text = ("Program:\tSzybki\nTemperatura:\t30 \u00B0C \nCzas:\t\t20 min\nIntensywność:\t800");
                temperature = 30;
                time = 20;
                whirlSpeed = 800;
                washProgram = "fast";
            }
            else
            {
                displayTextBox.Text = ("Program:\tCodzienny\nTemperatura: 30 \u00B0C \nCzas:\t\t60 min\nIntensywność:\t600");
                temperature = 30;
                time = 60;
                whirlSpeed = 600;
                washProgram = "everyday";
            }
            progressBarTime.Maximum = time;
            progressBarTemperature.Maximum = temperature;
        }

        private void ChangeWashProgram(object sender, RoutedEventArgs e)
        {
            ChangeWashProgramText();
        }
        private void OpenDoor()
        {
            if (buttonOpenDoor.Content.ToString() == "Otwórz drzwi")
            {
                buttonOpenDoor.Content = "Zamknij drzwi";
                buttonStart.IsEnabled = false;
                buttonStop.IsEnabled = false;
                buttonEmpty.IsEnabled = true;
                buttonInsertDish.IsEnabled = true;
                imageDoorClosed.Visibility = Visibility.Collapsed;
                imageDoorOpened.Visibility = Visibility.Visible;
            }
            else
            {
                buttonOpenDoor.Content = "Otwórz drzwi";
                buttonStart.IsEnabled = true;
                buttonStop.IsEnabled = true;
                buttonInsertDish.IsEnabled = false;
                buttonEmpty.IsEnabled = false;
                imageDoorClosed.Visibility = Visibility.Visible;
                imageDoorOpened.Visibility = Visibility.Collapsed;
            }
        }
        private void WaterPumpLevel(object sender, int e)
        {
            Dispatcher.Invoke(() => { waterLevel.Text = e.ToString(); });
        }
        private void TemperatureDegrees(object sender, int e)
        {
            Dispatcher.Invoke(() => { temperatureText.Text = e.ToString(); });
        }
        private void TimerTime(object sender, int e)
        {
            Dispatcher.Invoke(() => { 
                washTime.Text = e.ToString(); 
                if (e == 0)
                {
                    buttonOpenDoor.IsEnabled = true;
                }
            });
        }
        private void Laundry(object sender, int e)
        {
            Dispatcher.Invoke(() => { laundry = e; });
        }

        private void AddLaundry(object sender, RoutedEventArgs e)
        {
            weightL++;
            Task.Run(() => { weightSensorClient.SimulateCapacitySensor(laundry).Wait(); });
        }
        private void EmptyLaundry(object sender, RoutedEventArgs e)
        {
            weightL = 0;
            Task.Run(() => { weightSensorClient.SimulateCapacitySensorOut(laundry).Wait(); });
            ChangeWashProgramText();
        }
        private void OpenDoors(object sender, RoutedEventArgs e)
        {
            OpenDoor();
            Task.Run(() => { door.SimulateDoor().Wait(); });
        }
        private void StartWashing(object sender, RoutedEventArgs e)
        {
            if(weightL < 11)
            {
                buttonOpenDoor.IsEnabled = false;
                Task.Run(() => { washingMachine.TurnOn(time, water, temperature,whirlSpeed).Wait(); });
            }
            else
            {
                displayTextBox.Text = "Zbyt ciężkie pranie wyjmij";
            }
        }
        private void EndWashing(object sender, RoutedEventArgs e)
        {
            Task.Run(() => { washingMachine.TurnOf().Wait(); });
            buttonOpenDoor.IsEnabled = true;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            timer.DisposeAsync();
            door.DisposeAsync();
            waterPump.DisposeAsync();
        }
        
        
    }
}
