namespace IoTSimulation.Models
{
    internal sealed class SensorReadings
    {
        public decimal Temperature { get; set; }
        public decimal Humidity { get; set; }
        public decimal SoundLevel { get; set; }
        public decimal LightLevel { get; set; }
        public decimal Distance { get; set; }
    }
}
