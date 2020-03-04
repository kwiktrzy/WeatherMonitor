using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProjektInz.Data;
using ProjektInz.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjektInz
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private Timer _timer;
        private IServiceScopeFactory _provider;

        public TimedHostedService(ILogger<TimedHostedService> logger, IServiceScopeFactory serviceProvider)
        {
            _logger = logger;
            _provider = serviceProvider;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            //// GetWeatherDataApi getWeatherDataFromOutsideApi = new GetWeatherDataApi();
            // _logger.LogInformation("Microservice saving api data working");
            // _timer = new Timer(getWeatherDataFromOutsideApi.FillWeatherDataAsync, null, TimeSpan.Zero, TimeSpan.FromSeconds(60));
            // return Task.CompletedTask;
            _logger.LogInformation("Microservice saving api data working");
            using (IServiceScope scope = _provider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DevDbContext>();
                SaveWeather sw = new SaveWeather(context); 
                _timer = new Timer(sw.FillDataBase, null, TimeSpan.Zero, TimeSpan.FromSeconds(60));
            }
            return Task.CompletedTask;

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Microservice saving api data stopped");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
        public void Dispose()
        {
            _timer?.Dispose();
        }

        //https://blog.maartenballiauw.be/post/2017/08/01/building-a-scheduled-cache-updater-in-aspnet-core-2.html
    }
}
