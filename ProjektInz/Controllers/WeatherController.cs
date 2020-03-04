using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProjektInz.Data.Models;
using ProjektInz.ViewModels;

namespace ProjektInz.Controllers
{
    [Route("api/[controller]")]
    public class WeatherController : Controller
    {
        private DevDbContext _dbContext;
        private ControllersDataAdjustHelper _adjustHelper; 
        public WeatherController(DevDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Just temperature
        /// </summary>
        /// <returns></returns>
        [HttpGet("SensorReadTemperature")]
        public IActionResult TodaySensorReadTemperature()
        {
            DateTime date = DateTime.Now.Date;
            var sensorRead = _dbContext.SensorRead.Where(e => e.ReadDate >= date 
            && e.ReadDate < date.AddDays(1)).ToList(); ; //gets all reads in db
            _adjustHelper = new ControllersDataAdjustHelper();
            _adjustHelper.AxisLabelsAndReadings(15, sensorRead.Select(q => q.ReadDate).ToList(), 
                sensorRead.Select(z => z.Temperature).ToList());
            if (_adjustHelper.ListOfYs == null)
            {
                return NotFound(new
                {
                    Error = string.Format("Wystąpił problem, skontaktuj się z administratorem strony")
                });
            }
            return new JsonResult(_adjustHelper, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }
        [HttpGet("SensorReadTemperatureByDate/{dateTime}")]
        public IActionResult SensorReadTemperatureByDate(string dateTime)
        {
            DateTime date = DateTime.Parse(dateTime);
            var sensorRead = _dbContext.SensorRead.Where(e => e.ReadDate >= date
            && e.ReadDate < date.AddDays(1)).ToList(); ; //gets all reads in db
            _adjustHelper = new ControllersDataAdjustHelper();
            _adjustHelper.AxisLabelsAndReadings(60, sensorRead.Select(q => q.ReadDate).ToList(),
                sensorRead.Select(z => z.Temperature).ToList());
            if (_adjustHelper.ListOfYs == null)
            {
                return NotFound(new
                {
                    Error = string.Format("Wystąpił problem, skontaktuj się z administratorem strony")
                });
            }
            return new JsonResult(_adjustHelper, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }
        /// <summary>
        /// Just humidity
        /// </summary>
        /// <returns></returns>
        [HttpGet("TodaySensorReadHumidity")]
        public IActionResult TodaySensorReadHumidity()
        {
            DateTime date = DateTime.Now.Date;
            var sensorRead = _dbContext.SensorRead.Where(e => e.ReadDate >= date && e.ReadDate < date.AddDays(1)).ToList(); ; //gets all reads in db
            _adjustHelper = new ControllersDataAdjustHelper();
            _adjustHelper.AxisLabelsAndReadings(15, sensorRead.Select(q => q.ReadDate).ToList(), sensorRead.Select(z => z.Humidity).ToList());
            if (_adjustHelper.ListOfYs == null)
            {
                return NotFound(new
                {
                    Error = string.Format("Wystąpił problem, skontaktuj się z administratorem strony")
                });
            }
            return new JsonResult(_adjustHelper, new JsonSerializerSettings { Formatting = Formatting.Indented });

        }
        [HttpGet("SensorReadHumidityByDate/{dateTime}")]
        public IActionResult SensorReadHumidityByDate(string dateTime)
        {
            DateTime date = DateTime.Parse(dateTime);
            var sensorRead = _dbContext.SensorRead.Where(e => e.ReadDate >= date && e.ReadDate < date.AddDays(1)).ToList(); ; //gets all reads in db
            _adjustHelper = new ControllersDataAdjustHelper();
            _adjustHelper.AxisLabelsAndReadings(60, sensorRead.Select(q => q.ReadDate).ToList(), sensorRead.Select(z => z.Humidity).ToList());
            if (_adjustHelper.ListOfYs == null)
            {
                return NotFound(new
                {
                    Error = string.Format("Wystąpił problem, skontaktuj się z administratorem strony")
                });
            }
            return new JsonResult(_adjustHelper, new JsonSerializerSettings { Formatting = Formatting.Indented });

        }
        [HttpGet("ForecastByDate/{dateTime}")]
        public IActionResult ForecastByDate(string dateTime)
        {
             DateTime date = DateTime.Parse(dateTime);//DateTime.UtcNow;
 
            var forecastRead = _dbContext.ForecastWeatherFromApi.Where(e => e.DtTxt >= date && e.DtTxt < date.AddDays(1)).ToList(); ; //gets all reads in db

            _adjustHelper = new ControllersDataAdjustHelper();
            //why 60 cuz need to have the same time gaps in compare view.   
            _adjustHelper.AxisLabelsAndReadings(60, forecastRead.Select(q => q.DtTxt).ToList(), forecastRead.Select(z => z.Temperature).ToList());
            if (_adjustHelper.ListOfYs == null)
            {
                return NotFound(new
                {
                    Error = string.Format("Wystąpił problem, skontaktuj się z administratorem strony")
                });
            }
            return new JsonResult(_adjustHelper, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }
        [HttpGet("CurrentWeatherByDate/{dateTime}")]
        public IActionResult CurrentWeatherByDate(string dateTime)
        { 
            DateTime date = DateTime.Parse(dateTime);//DateTime.UtcNow;

            var currentWeathersRead = _dbContext.CurrentWeatherFromApi.Where(e => e.Date >= date && e.Date < date.AddDays(1)).ToList(); ; //gets all reads in db

            _adjustHelper = new ControllersDataAdjustHelper();   
            _adjustHelper.AxisLabelsAndReadings(currentWeathersRead.Select(q => q.Date).ToList(), currentWeathersRead.Select(z => z.Temperature).ToList());
            if (_adjustHelper.ListOfYs == null)
            {
                return NotFound(new
                {
                    Error = string.Format("Wystąpił problem, skontaktuj się z administratorem strony")
                });
            }
            return new JsonResult(_adjustHelper, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }
        //TODO: do the same for Pressure and Humidity
    }
}