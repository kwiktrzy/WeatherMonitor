using System;
using System.Collections.Generic;

namespace ProjektInz.Data.Models
{
    public partial class SensorRead
    {
        public SensorRead()
        {

        }

        public int Id { get; set; }
        public string SensorName { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double Pressure { get; set; }
        public double Altitude { get; set; }
        public DateTime ReadDate { get; set; }
    }
}
