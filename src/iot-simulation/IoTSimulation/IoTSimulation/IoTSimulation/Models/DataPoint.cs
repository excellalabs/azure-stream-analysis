namespace IoTSimulation.Models
{
    internal sealed class DataPoint
    {
        public string DeviceId { get; set; }

        public SensorReadings SensorReadings { get; set; }

        public override string ToString()
        {
            return $"DeviceId: {DeviceId}, " +
                $"Temperature: {SensorReadings.Temperature}, " +
                $"Humidity: {SensorReadings.Humidity}, " +
                $"SoundLevel: {SensorReadings.SoundLevel}, " +
                $"LightLevel: {SensorReadings.LightLevel}," +
                $"Distance: {SensorReadings.Distance}";
        }
    }
}
