using System;

namespace Tamagotchi.Domain.Exceptions
{
    public class TamagotchiDomainException : Exception
    {
        public TamagotchiDomainException()
        { }

        public TamagotchiDomainException(string message)
            : base(message)
        { }

        public TamagotchiDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
