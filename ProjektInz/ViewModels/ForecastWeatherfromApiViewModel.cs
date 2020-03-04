using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektInz.ViewModels
{

    [JsonObject(MemberSerialization.OptOut)]
    public class ForecastWeatherfromApiViewModel
    {
        public ForecastWeatherfromApiViewModel()
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
