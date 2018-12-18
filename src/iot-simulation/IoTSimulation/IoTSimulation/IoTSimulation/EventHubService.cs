using IoTSimulation.Models;
using Microsoft.Azure.EventHubs;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Excella.TwitterClient.Business.EventHub
{
    internal interface IEventHubService
    {
        Task SendMessageAsync(DataPoint dataPoint);
    }

    internal sealed class EventHubService : IEventHubService
    {
        private EventHubClient eventHubClient;
        private readonly EventHubSettings eventHubSettings;

        public EventHubService(EventHubSettings eventHubSettings)
        {
            this.eventHubSettings = eventHubSettings;
        }

        public async Task SendMessageAsync(DataPoint dataPoint)
        {
            if (this.eventHubClient == null)
            {
                InitializeEventHub();
            }

            var message = JsonConvert.SerializeObject(dataPoint);

            Console.WriteLine($"Sending simulated data point: {dataPoint.ToString()}");

            await this.eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(message)));
        }

        private void InitializeEventHub()
        {
            var connectionStringBuilder = new EventHubsConnectionStringBuilder(eventHubSettings.EventHubConnectionString)
            {
                EntityPath = eventHubSettings.EventHubPath
            };
            this.eventHubClient = EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());
        }
    }
}