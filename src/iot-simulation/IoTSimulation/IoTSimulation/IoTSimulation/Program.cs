using Excella.TwitterClient.Business.EventHub;
using IoTSimulation.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading;

namespace IoTSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            var configurationRoot = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                .Build();

            var appSettings = configurationRoot.Get<AppSettings>();

            Console.WriteLine("Starting IoT simulation");
            var eventHubService = new EventHubService(appSettings.EventHubSettings);

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
