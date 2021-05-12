using Tamagotchi.Domain.Common;

namespace Tamagotchi.Domain.AggregatesModel.DragonAggregate
{
    public class LifeStage : Enumeration
    {
        public static LifeStage Baby = new(1, nameof(Baby));
        public static LifeStage Child = new(2, nameof(Child));
        public static LifeStage Teen = new(3, nameof(Teen));
        public static LifeStage Adult = new(4, nameof(Adult));

        public LifeStage(int id, string name) : base(id, name)
        {
        }
    }
}
