using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagotchi.Domain.Strategies.DragonMetrics
{
    public class ChildDragonMetrics : IDragonMetrics
    {
        public string Name => nameof(ChildDragonMetrics);

        public int GetHappinessMetricRate()
        {
            return 2;
        }

        public int GetHungerMetricRate()
        {
            return 2;
        }
    }
}
