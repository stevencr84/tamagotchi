using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tamagotchi.Application.Application.Queries
{
    public class DragonViewModel
    {
        public int DragonId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Happiness { get; set; }
        public int Hunger { get; set; }
        public string LifeStage { get; set; }
    }
}
