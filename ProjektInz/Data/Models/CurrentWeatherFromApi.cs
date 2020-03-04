using System;
using System.Collections.Generic;

namespace ProjektInz.Data.Models
{
    public partial class CurrentWeatherFromApi
    {
        public int Id { get; set; }
        public double Temperature { get; set; }
        public double Pressure { get; set; }
        public double Humidty { get; set; }
        public double? TempMin { get; set; }
        public double? TempMax { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
