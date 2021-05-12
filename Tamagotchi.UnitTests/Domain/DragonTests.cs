using FluentAssertions;
using System.Collections.Generic;
using Tamagotchi.Domain.AggregatesModel.DragonAggregate;
using Tamagotchi.Domain.Common;
using Tamagotchi.Domain.Exceptions;
using Xunit;

namespace Tamagotchi.UnitTests
{
    public class DragonTests
    {
        private static readonly string DragonName = "My Test Dragon";

        [Fact]
        public void Create_Dragon_Starts_As_Baby_And_Neutral_Levels_Of_Happiness_And_Hunber()
        {
            //Arrange
            var expectedLifeStage = LifeStage.Baby;
            var expectedHungerLevel = 5;
            var expectedHappinessLevel = 5;
            
            var dragon = new Dragon(DragonName);

            //Act 

            //Assert
            dragon.GetCurrentLifeStage().Should().Be(expectedLifeStage);
            dragon.Happiness.Should().Be(expectedHappinessLevel);
            dragon.Hunger.Should().Be(expectedHungerLevel);
        }

        [Fact]
        public void Create_Dragon_With_Empty_Name_Throws_Exception()
        {
            //Arrange

            //Act 

            //Assert
            Assert.Throws<TamagotchiDomainException>(() => new Dragon(string.Empty));
        }

        [Fact]
        public void When_Pet_Happiness_Should_Increase()
        {
            //Arrange
            var initialHappinessLevel = 5;

            var dragon = new Dragon(DragonName);

            //Act
            dragon.Pet();

            //Assert
            dragon.Happiness.Should().BeGreaterThan(initialHappinessLevel);
        }

        [Fact]
        public void When_Feed_Hunger_Should_Decrease()
        {
            //Arrange
            var initialHungerLevel = 5;

            var dragon = new Dragon(DragonName);

            //Act
            dragon.Feed();

            //Assert
            dragon.Hunger.Should().BeLessThan(initialHungerLevel);
        }

        [Fact]
        public void When_Grows_Happiness_Should_Decrease()
        {
            //Arrange
            var initialHappinessLevel = 5;

            var dragon = new Dragon(DragonName);

            //Act
            dragon.Grows();

            //Assert
            dragon.Happiness.Should().BeLessThan(initialHappinessLevel);
        }

        [Fact]
        public void When_Grows_Hunger_Should_Increase()
        {
            //Arrange
            var initialHungerLevel = 5;

            var dragon = new Dragon(DragonName);

            //Act
            dragon.Grows();

            //Assert
            dragon.Hunger.Should().BeGreaterThan(initialHungerLevel);
        }

        [Theory]
        [InlineData(Constants.BabyAge, 1)]
        [InlineData(Constants.ChildAge, 2)]
        [InlineData(Constants.TeenAge, 3)]
        [InlineData(Constants.AdultAge, 4)]
        public void When_Specific_Age_Should_Change_LifeStage_To_Expected_Stage(int age, int expectedStageId)
        {
            //Arrange
            var dragon = new Dragon(DragonName, age);

            //Act

            //Assert
            dragon.GetCurrentLifeStage().Id.Should().Be(expectedStageId);
        }

        [Theory, MemberData(nameof(DragonStages))]
        public void When_Grows_At_Different_Life_Stage_Hunger_Level_Should_Be_Different(Dragon dragon1, Dragon dragon2)
        {
            //Act
            dragon1.Grows();
            dragon2.Grows();

            //Assert
            dragon1.Hunger.Should().NotBe(dragon2.Hunger);
        }

        public static IEnumerable<object[]> DragonStages()
        {
            var babyDragon = new Dragon(DragonName, Constants.BabyAge);
            var childDragon = new Dragon(DragonName, Constants.ChildAge);
            var teenDragon = new Dragon(DragonName, Constants.TeenAge);
            var adultDragon = new Dragon(DragonName, Constants.AdultAge);

            //All diferent combinations of dragon stages
            return new[]
            {
                new object[] { babyDragon, childDragon },
                new object[] { babyDragon, teenDragon },
                new object[] { babyDragon, adultDragon },
                new object[] { childDragon, teenDragon },
                new object[] { childDragon, adultDragon },
                new object[] { teenDragon, adultDragon }
            };
        }
    }
}
