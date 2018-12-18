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
        private readonly EventHubClient eventHubClient;

        public EventHubService()
        {
            this.eventHubClient = InitializeEventHub();
        }

        public async Task SendMessageAsync(DataPoint dataPoint)
        {
            var message = JsonConvert.SerializeObject(dataPoint);

            Console.WriteLine("Sending simulated data point");

            await this.eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(message)));
        }

        private EventHubClient InitializeEventHub()
        {
            var ehConnectionString = "";
            var eventHubPath = "";

            var connectionStringBuilder = new EventHubsConnectionStringBuilder(ehConnectionString)
            {
                EntityPath = eventHubPath
            };
            return EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());
        }
    }
}