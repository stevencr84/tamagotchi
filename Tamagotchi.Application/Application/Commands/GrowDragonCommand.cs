using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tamagotchi.Application.Application.Commands
{
    public class GrowDragonCommand
    {
        public int DragonId { get; set; }

        public GrowDragonCommand() { }

        public GrowDragonCommand(int id)
        {
            DragonId = id;
        }
    }
}
