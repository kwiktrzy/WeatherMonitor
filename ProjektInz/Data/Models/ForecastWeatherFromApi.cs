using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace ProjektInz.Data.Models
{    public partial class ForecastWeatherFromApi
    {
        public ForecastWeatherFromApi()
        {

        }
        public int Id { get; set; }
        public double Temperature { get; set; }
        public double Pressure { get; set; }
        public double Humidty { get; set; }
        public double? TempMin { get; set; }
        public double? TempMax { get; set; }
        public string Description { get; set; }
        public DateTime DtTxt { get; set; }
        [JsonIgnore]
        public DateTime ReadDate { get; set; }
      
    }
}
