using System;
using System.Collections.Generic;
using System.Text;

namespace IoTSimulation.Models
{
    internal sealed class DataPoint
    {
        public string DeviceId { get; set; }

        public SensorReadings SensorReadings { get; set; }
    }
}
