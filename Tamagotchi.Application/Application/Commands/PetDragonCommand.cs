using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tamagotchi.Application.Application.Commands
{
    public class PetDragonCommand
    {
        public int DragonId { get; set; }

        public PetDragonCommand() { }

        public PetDragonCommand(int id)
        {
            DragonId = id;
        }
    }
}