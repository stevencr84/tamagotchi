using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagotchi.Domain.Strategies.DragonMetrics
{
    public class BabyDragonMetrics : IDragonMetrics
    {
        public string Name => nameof(BabyDragonMetrics);

        public int GetHappinessMetricRate()
        {
            return 1;
        }

        public int GetHungerMetricRate()
        {
            return 1;
        }
    }
}
