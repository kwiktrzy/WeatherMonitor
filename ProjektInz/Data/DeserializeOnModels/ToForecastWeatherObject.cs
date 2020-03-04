using Newtonsoft.Json.Linq;
using ProjektInz.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektInz.Data.DeserializeOnModels
{
    public static class ToForecastWeatherObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonObject"></param>
        /// <returns>40 records temp etc.. </returns>
        public static List<ForecastWeatherFromApi> Deserialize(JObject jsonObject)
        {
            List<ForecastWeatherFromApi> forecastWeathers = new List<ForecastWeatherFromApi>();
            for (int i = 0; i < 40; i++)
            {
                //TODO: if(Convert.ToString(jsonObject.SelectToken($"list[{i}].dt_txt")) ==     //if value have same time in db what should i do? keep both as right now?
                ForecastWeatherFromApi forecastObject = new ForecastWeatherFromApi();
                forecastObject.Temperature = Convert.ToDouble(jsonObject.SelectToken($"list[{i}].main.temp"));
                forecastObject.TempMax = Convert.ToDouble(jsonObject.SelectToken($"list[{i}].main.temp_max"));
                forecastObject.TempMin = Convert.ToDouble(jsonObject.SelectToken($"list[{i}].main.temp_min"));
                forecastObject.Pressure = Convert.ToDouble(jsonObject.SelectToken($"list[{i}].main.pressure"));
                forecastObject.Humidty = Convert.ToDouble(jsonObject.SelectToken($"list[{i}].main.humidity"));
                forecastObject.Description = Convert.ToString(jsonObject.SelectToken($"list[{i}].weather[0].description"));
                forecastObject.DtTxt = Convert.ToDateTime(jsonObject.SelectToken($"list[{i}].dt_txt"));
                forecastObject.ReadDate = DateTime.Now;
                forecastWeathers.Add(forecastObject);
            }
            return forecastWeathers;
        }
    }
}
