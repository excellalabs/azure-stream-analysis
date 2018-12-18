namespace IoTSimulation.Models
{
    internal sealed class AppSettings
    {
        public EventHubSettings EventHubSettings { get; set; }
    }

    internal sealed class EventHubSettings
    {
        public string EventHubPath { get; set; }
        public string EventHubConnectionString { get; set; }
    }
}
