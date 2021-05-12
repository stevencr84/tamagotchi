using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Tamagotchi.Application.Application.BackgroundTasks
{
    public interface IWorker
    {
        Task DoWork(CancellationToken cancellationToken);
    }
}
