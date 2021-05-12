using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Tamagotchi.Application.Application.BackgroundTasks
{
    public class DragonGrowthBackgroundService : BackgroundService
    {
        private readonly IWorker _worker;

        public DragonGrowthBackgroundService(IWorker worker)
        {
            _worker = worker;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _worker.DoWork(stoppingToken);
        }
    }
}
