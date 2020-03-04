using Newtonsoft.Json.Linq;
using ProjektInz.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektInz.Data.DeserializeOnModels
{
    public static class ToCurrentWeatherObject
    {
        /// <summary>
        ///  
        /// </summary> 
        /// <param name="jsonObject"></param>
        /// <returns>One current weather object</returns>
        public static CurrentWeatherFromApi Deserialize(JObject jsonObject)
        {
            CurrentWeatherFromApi currentWeatherObject = new CurrentWeatherFromApi();
            currentWeatherObject.Temperature = Convert.ToDouble(jsonObject.SelectToken("main.temp"));
            currentWeatherObject.TempMax = Convert.ToDouble(jsonObject.SelectToken("main.temp_max"));
            currentWeatherObject.TempMin = Convert.ToDouble(jsonObject.SelectToken("main.temp_min"));
            currentWeatherObject.Pressure = Convert.ToDouble(jsonObject.SelectToken($"main.pressure"));
            currentWeatherObject.Humidty = Convert.ToDouble(jsonObject.SelectToken($"main.humidity"));
            currentWeatherObject.Description = Convert.ToString(jsonObject.SelectToken($"weather[0].description"));
            currentWeatherObject.Date = DateTime.Now;
            return currentWeatherObject;
        }
    }
}
