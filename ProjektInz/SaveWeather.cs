using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using ProjektInz.Data.DeserializeOnModels;
using ProjektInz.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjektInz
{
    public static class SaveWeather
    {
        /// <summary>
        /// Parse JObject on db object and push it there
        /// </summary>
        /// <param name="forecastWeather"></param>
        public static void FillOutDbWithForecast(JObject forecastWeather, DevDbContext dbContext)
        {
            try
            {
                dbContext.ForecastWeatherFromApi.AddRange(ToForecastWeatherObject.Deserialize(forecastWeather));
                dbContext.SaveChanges(); 
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// Parse JObject on db object and push it there
        /// </summary>
        /// <param name="currentWeather"></param>
        public static void FillOutDbWithCurrentWeather(JObject currentWeather, DevDbContext dbContext)
        {
            try
            {
                dbContext.CurrentWeatherFromApi.Add(ToCurrentWeatherObject.Deserialize(currentWeather));
                dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// Saves collection List<SensorRead> in database 
        /// </summary>
        /// <param name="sensorReads"></param>
        /// <param name="dbContext"></param>
        public static void FillOutDbWithSensorRead(List<SensorRead> sensorReads, DevDbContext dbContext)
        { 
            try
            {
                dbContext.SensorRead.AddRange(sensorReads);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
    
}
