using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagotchi.Domain.Strategies.DragonMetrics
{
    public class AdultDragonMetrics : IDragonMetrics
    {
        public string Name => nameof(AdultDragonMetrics);

        public int GetHappinessMetricRate()
        {
            return 4;
        }

        public int GetHungerMetricRate()
        {
            return 4;
        }
    }
}
