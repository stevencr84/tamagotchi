using System.Linq;
using Tamagotchi.Domain.Common;
using Tamagotchi.Domain.Exceptions;
using Tamagotchi.Domain.Strategies.DragonMetrics;

namespace Tamagotchi.Domain.AggregatesModel.DragonAggregate
{
    public class Dragon : Entity, IAggregateRoot
    {
        //Dragon's age increase count in months
        private const int AgeStepCount = 4;
        public string Name { get; }
        public int Age { get; private set; }
        public double Weight { get; }
        public int Happiness { get; private set; }
        public int Hunger { get; private set; }
        private int _lifeStageId;
        public LifeStage LifeStage { get; private set; }

        public Dragon(string name)
        {
            this.Name = !string.IsNullOrWhiteSpace(name) ? name : throw new TamagotchiDomainException(nameof(name));
            this._lifeStageId = LifeStage.Baby.Id;
            this.Happiness = 5;
            this.Hunger = 5;
            this.Age = 0;
        }

        public Dragon(string name, int age)
        {
            this.Name = name;
            this.Happiness = 5;
            this.Hunger = 5;

            this.SetAge(age);
        }

        public void Grows()
        {
            this.SetAge(this.Age + AgeStepCount);

            var metrics = DragonMetricsFactory.GetMetrics(_lifeStageId);

            this.Happiness -= metrics.GetHappinessMetricRate();
            this.Hunger += metrics.GetHungerMetricRate();
        }

        public void Pet()
        {
            var metrics = DragonMetricsFactory.GetMetrics(_lifeStageId);

            this.Happiness += metrics.GetHappinessMetricRate();
        }

        private void SetAge(int age)
        {
            this.Age = age;

            this.CalculateLifeStage();
        }

        public void Feed()
        {
            var metrics = DragonMetricsFactory.GetMetrics(_lifeStageId);

            this.Hunger -= metrics.GetHungerMetricRate();
        }

        private void CalculateLifeStage()
        {

            if (this.Age == Constants.BabyAge)
            {
                _lifeStageId = LifeStage.Baby.Id;
            }
            else if (this.Age == Constants.ChildAge)
            {
                _lifeStageId = LifeStage.Child.Id;
            }
            else if (this.Age == Constants.TeenAge)
            {
                _lifeStageId = LifeStage.Teen.Id;
            }
            else if (this.Age == Constants.AdultAge)
            {
                _lifeStageId = LifeStage.Adult.Id;
            }
        }

        public LifeStage GetCurrentLifeStage()
        {
            return Enumeration.GetAll<LifeStage>().FirstOrDefault(ls => ls.Id == _lifeStageId);
        }
    }
}
