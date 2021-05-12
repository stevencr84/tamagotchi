using Akka.Actor;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Tamagotchi.Application.Application.Commands;
using Tamagotchi.Application.Application.Providers;
using Tamagotchi.Application.Application.Queries;

namespace Tamagotchi.Application.Application.BackgroundTasks
{
    public class Worker : IWorker
    {
        private readonly ILogger<Worker> _logger;
        private readonly IDragonQueries _dragonQueries;
        private readonly IActorRef _dragonActor;
        public Worker(ILogger<Worker> logger, IDragonQueries dragonQueries, IDragonActorProvider dragonActorProvider)
        {
            _logger = logger;
            _dragonQueries = dragonQueries;
            _dragonActor = dragonActorProvider.Get();
        }

        public async Task DoWork(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var dragons = await _dragonQueries.GetDragonsAsync();

                foreach(var dragon in dragons)
                {
                    var command = new GrowDragonCommand() { DragonId = dragon.DragonId };
                    _dragonActor.Tell(command);

                    _logger.LogInformation($"Worker has increased age of draggon id {dragon.DragonId}");
                }

                
                await Task.Delay(1000 * 60);
            }
        }
    }
}
