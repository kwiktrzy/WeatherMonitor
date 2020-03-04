using Newtonsoft.Json.Linq;
using ProjektInz.Data.DeserializeOnModels;
using ProjektInz.Data.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System.Resources;

namespace ProjektInz
{
    /// <summary>
    /// Uses to get weather to save in db
    /// </summary>
    class GetWeather
    {
        public JObject Forecast { get; private set; }
        public JObject CurrentWeather { get; private set; }
        public List<SensorRead> SensorReads { get; private set; }

        public GetWeather()
        {
            Forecast = GetForecast();
            CurrentWeather = GetCurrentWeather();
            SensorReads = GetSensorReadList();
        }
        /// <summary>
        /// Gets from openweathermap forecast and parse on JObject
        /// </summary>
        /// <returns></returns>
        private JObject GetForecast()
        {
            using (var webClient = new WebClient())
            {
                var forecast = JObject.Parse(webClient.DownloadString(Properties.Resources.GetForecast));
                return forecast;
            }
        }
        /// <summary>
        /// Gets from openweathermap api current weather and parse on JObject
        /// </summary>
        /// <returns></returns>
        private JObject GetCurrentWeather()
        {
            using (var webClient = new WebClient())
            {
                var currWeather = JObject.Parse(webClient.DownloadString(Properties.Resources.GetCurrentWeather)); //TODO: write it in secret.. 
                return currWeather;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<SensorRead> GetSensorReadList()
        {
            List<SensorRead> list = new List<SensorRead>();
            list = ToSensorReadObject.SavedSensorReads;
            ToSensorReadObject.ClearList();
            return list;
        }

    }
   }



