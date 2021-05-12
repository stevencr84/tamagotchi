using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagotchi.Domain.Strategies.DragonMetrics
{
    public class TeenDragonMetrics : IDragonMetrics
    {
        public string Name => nameof(TeenDragonMetrics);

        public int GetHappinessMetricRate()
        {
            return 3;
        }

        public int GetHungerMetricRate()
        {
            return 3;
        }
    }
}
