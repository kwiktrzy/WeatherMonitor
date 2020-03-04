using Microsoft.Extensions.DependencyInjection;
using ProjektInz.Data;
using ProjektInz.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjektInz
{

    public class RepetableAdd : BackgroundService
    {
        public RepetableAdd(IServiceScopeFactory scopeFactory) : base(scopeFactory)
        {
        }
  
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<DevDbContext>();
                    Console.WriteLine("Service running");
                    GetWeather gw = new GetWeather();
                    SaveWeather.FillOutDbWithForecast(gw.Forecast,dbContext);
                    SaveWeather.FillOutDbWithCurrentWeather(gw.CurrentWeather, dbContext);
                    SaveWeather.FillOutDbWithSensorRead(gw.SensorReads, dbContext);
                    await Task.Delay(3600000, stoppingToken); //hour
                    //await Task.Delay(60000, stoppingToken);
                }
            }
        }

    }
}
