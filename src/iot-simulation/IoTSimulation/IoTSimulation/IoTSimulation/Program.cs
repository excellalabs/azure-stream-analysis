using Excella.TwitterClient.Business.EventHub;
using IoTSimulation.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;

namespace IoTSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting IoT simulation");
            var eventHubService = new EventHubService();

            while (true)
            {
                var dataPoint = new DataPoint
                {
                    DeviceId = "SimulatedDevice",
                    SensorReadings = new SensorReadings
                    {
                        Temperature = 20.5m,
                        Humidity = 80,
                        LightLevel = 300,
                        SoundLevel = new Random().Next(0, 100),
                        Distance = new Random().Next(1, 200)
                    }
                };

                eventHubService.SendMessageAsync(dataPoint);
                Thread.Sleep(1000);
            }
        }
    }
}
