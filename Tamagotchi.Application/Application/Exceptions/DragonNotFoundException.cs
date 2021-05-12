using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tamagotchi.Application.Application.Exceptions
{
    public class DragonNotFoundException : Exception
    {
        public DragonNotFoundException()
        { }

        public DragonNotFoundException(string message)
            : base(message)
        { }

        public DragonNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
