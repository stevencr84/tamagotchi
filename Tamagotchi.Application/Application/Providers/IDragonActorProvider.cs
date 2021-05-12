using Akka.Actor;

namespace Tamagotchi.Application.Application.Providers
{
    public interface IDragonActorProvider
    {
        IActorRef Get();
    }
}
