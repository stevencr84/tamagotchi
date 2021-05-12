using Akka.Actor;
using Tamagotchi.Application.Application.Actors;
using Tamagotchi.Domain.AggregatesModel.DragonAggregate;

namespace Tamagotchi.Application.Application.Providers
{
    public class DragonActorProvider : IDragonActorProvider
    {
        IActorRef _dragonActor; 
        public DragonActorProvider(IActorRefFactory actorSystem, IDragonRepository dragonRepository)
        {
            _dragonActor = actorSystem.ActorOf(Props.Create<DragonActor>(dragonRepository), "dragons");
        }

        public IActorRef Get()
        {
            return _dragonActor;
        }
    }
}
