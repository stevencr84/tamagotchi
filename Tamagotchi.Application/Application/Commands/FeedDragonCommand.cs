using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tamagotchi.Application.Application.Commands
{
    public class FeedDragonCommand
    {
        public int DragonId { get; set; }

        public FeedDragonCommand() { }

        public FeedDragonCommand(int id)
        {
            DragonId = id;
        }
    }
}