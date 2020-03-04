using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjektInz.Data.DeserializeOnModels;
using ProjektInz.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProjektInz
{
    public class GetWeatherFromApi
    {
        public JObject Forecast { get; private set; }
        public JObject CurrentWeather { get; private set; }

        public GetWeatherFromApi()
        {
             
        }
        public async Task FillDatabaseAsync()
        { //(object state)
            try
            {
                Forecast = new JObject();
                CurrentWeather = new JObject();
                Forecast = await GetForecastAsync().ConfigureAwait(false);
                CurrentWeather = await GetCurrentForecastAsync().ConfigureAwait(false);
            }
            catch
            {
                throw;
            }  
        }
        /// <summary>
        /// Gets from api and parse on JObject
        /// </summary>
        /// <returns></returns>
        async Task<JObject> GetForecastAsync()
        {
            using (var httpClient = new HttpClient())
            {
                return JObject.Parse(await httpClient.GetStringAsync("http://api.openweathermap.org/data/2.5/forecast?id=7532290&units=metric&APPID=81716797f34113e2342ff9c2c7dbed58").ConfigureAwait(false));
            }
        }
        /// <summary>
        /// Gets from api and parse 
        /// </summary>
        /// <returns></returns>
        [Obsolete("Check corectness this method. Prolly wrongly gets data")]
        async Task<JObject> GetCurrentForecastAsync()
        {
            using (var httpClient = new HttpClient())
            {
                return JObject.Parse(await httpClient.GetStringAsync("http://api.openweathermap.org/data/2.5/weather?id=7532290&units=metric&APPID=81716797f34113e2342ff9c2c7dbed58").ConfigureAwait(false));
            };
        }
    }
}
