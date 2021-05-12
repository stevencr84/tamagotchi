namespace Tamagotchi.Domain.Strategies.DragonMetrics
{
    public class DragonMetricsFactory
    {
        public static IDragonMetrics GetMetrics(int lifeStageId)
        {
            IDragonMetrics metrics = null;
            
            switch(lifeStageId)
            {
                case 1:
                    metrics = new BabyDragonMetrics();
                    break;
                case 2:
                    metrics = new ChildDragonMetrics();
                    break;
                case 3:
                    metrics = new TeenDragonMetrics();
                    break;
                case 4:
                    metrics = new AdultDragonMetrics();
                    break;
            }

            return metrics;
        }
    }
}
