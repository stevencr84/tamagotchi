using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagotchi.Domain.Strategies.DragonMetrics
{
    public interface IDragonMetrics
    {
        string Name { get; }
        int GetHappinessMetricRate();
        int GetHungerMetricRate();
    }
}
