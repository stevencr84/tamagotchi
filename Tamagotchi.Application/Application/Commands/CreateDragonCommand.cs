using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tamagotchi.Application.Application.Commands
{
    public class CreateDragonCommand
    {
        public string Name { get; set; }

        public CreateDragonCommand() { }

        public CreateDragonCommand(string name)
        {
            Name = name;
        }
    }
}