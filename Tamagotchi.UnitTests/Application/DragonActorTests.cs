using Akka.Actor;
using Akka.Event;
using Akka.TestKit;
using Akka.TestKit.Xunit;
using Moq;
using System;
using System.Threading.Tasks;
using Tamagotchi.Application.Application.Actors;
using Tamagotchi.Application.Application.Commands;
using Tamagotchi.Application.Application.Exceptions;
using Tamagotchi.Domain.AggregatesModel.DragonAggregate;
using Tamagotchi.Domain.Common;
using Xunit;

namespace Tamagotchi.UnitTests.Application
{
    public class DragonActorTests : TestKit
    {
        private readonly Mock<IDragonRepository> _dragonRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public DragonActorTests()
        {
            _dragonRepositoryMock = new Mock<IDragonRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task DragonActor_Receives_Create_Dragon_Command_Should_Return_Created_Dragon_Id()
        {
            //Arrange
            var fakeCreateDragonCommand = new CreateDragonCommand("Test Dragon");
            var fakeDragon = new Dragon(fakeCreateDragonCommand.Name);

            _unitOfWorkMock.Setup(x => x.SaveChangesAsync(default))
                .Returns(Task.FromResult(1));

            _dragonRepositoryMock.SetupGet(x => x.UnitOfWork).Returns(_unitOfWorkMock.Object);
            _dragonRepositoryMock.Setup(x => x.Add(It.IsAny<Dragon>())).Returns(fakeDragon);

            var dragonActor = Sys.ActorOf(Props.Create<DragonActor>(_dragonRepositoryMock.Object));

            //Act
            var result = await dragonActor.Ask(fakeCreateDragonCommand);

            Assert.IsType<int>(result);
        }

        [Fact]
        public void DragonActor_Receives_Pet_Dragon_Command_Should_Return_No_Message()
        {
            //Arrange
            var fakeDragonId = 1;
            var fakePetDragonCommand = new PetDragonCommand(fakeDragonId);
            var fakeDragon = new Dragon("Fake Dragon");

            _unitOfWorkMock.Setup(x => x.SaveChangesAsync(default))
                .Returns(Task.FromResult(1));

            _dragonRepositoryMock.SetupGet(x => x.UnitOfWork).Returns(_unitOfWorkMock.Object);
            _dragonRepositoryMock.Setup(x => x.Update(It.IsAny<Dragon>())).Returns(fakeDragon);

            var dragonActor = Sys.ActorOf(Props.Create<DragonActor>(_dragonRepositoryMock.Object));

            //Act
            dragonActor.Tell(fakePetDragonCommand);

            ExpectNoMsg();
        }

        [Fact]
        public void DragonActor_Receives_Pet_Dragon_NonExistent_Id_Should_Throw()
        {
            //Arrange
            var fakeDragonId = 1;
            var fakePetDragonCommand = new PetDragonCommand(fakeDragonId);
            Dragon nullDragon = null;

            _unitOfWorkMock.Setup(x => x.SaveChangesAsync(default))
                .Returns(Task.FromResult(1));

            _dragonRepositoryMock.SetupGet(x => x.UnitOfWork).Returns(_unitOfWorkMock.Object);
            _dragonRepositoryMock.Setup(x => x.GetAsync(It.IsAny<int>())).Returns(Task.FromResult(nullDragon));

            var dragonActor = Sys.ActorOf(Props.Create<DragonActor>(_dragonRepositoryMock.Object));

            var filter = CreateEventFilter(Sys);

            //Act
            dragonActor.Tell(fakePetDragonCommand);

            filter.Custom(logEvent => logEvent is Error).ExpectOne(() =>
            {
                Assert.True(true);
            });
        }

        [Fact]
        public void DragonActor_Receives_Feed_Dragon_Command_Should_Return_No_Message()
        {
            //Arrange
            var fakeDragonId = 1;
            var fakeFeedDragonCommand = new FeedDragonCommand(fakeDragonId);
            var fakeDragon = new Dragon("Fake Dragon");

            _unitOfWorkMock.Setup(x => x.SaveChangesAsync(default))
                .Returns(Task.FromResult(1));

            _dragonRepositoryMock.SetupGet(x => x.UnitOfWork).Returns(_unitOfWorkMock.Object);
            _dragonRepositoryMock.Setup(x => x.Update(It.IsAny<Dragon>())).Returns(fakeDragon);

            var dragonActor = Sys.ActorOf(Props.Create<DragonActor>(_dragonRepositoryMock.Object));

            //Act
            dragonActor.Tell(fakeFeedDragonCommand);

            ExpectNoMsg();
        }

        [Fact]
        public void DragonActor_Receives_Feed_Dragon_NonExistent_Id_Should_Throw()
        {
            //Arrange
            var fakeDragonId = 1;
            var fakeFeedDragonCommand = new FeedDragonCommand(fakeDragonId);
            Dragon nullDragon = null;

            _unitOfWorkMock.Setup(x => x.SaveChangesAsync(default))
                .Returns(Task.FromResult(1));

            _dragonRepositoryMock.SetupGet(x => x.UnitOfWork).Returns(_unitOfWorkMock.Object);
            _dragonRepositoryMock.Setup(x => x.GetAsync(It.IsAny<int>())).Returns(Task.FromResult(nullDragon));

            var dragonActor = Sys.ActorOf(Props.Create<DragonActor>(_dragonRepositoryMock.Object));

            var filter = CreateEventFilter(Sys);

            //Act
            dragonActor.Tell(fakeFeedDragonCommand);

            filter.Custom(logEvent => logEvent is Error).ExpectOne(() =>
            {
                Assert.True(true);
            });
        }

        [Fact]
        public void DragonActor_Receives_Grow_Dragon_Command_Should_Return_No_Message()
        {
            //Arrange
            var fakeDragonId = 1;
            var fakeGrowDragonCommand = new GrowDragonCommand(fakeDragonId);
            var fakeDragon = new Dragon("Fake Dragon");

            _unitOfWorkMock.Setup(x => x.SaveChangesAsync(default))
                .Returns(Task.FromResult(1));

            _dragonRepositoryMock.SetupGet(x => x.UnitOfWork).Returns(_unitOfWorkMock.Object);
            _dragonRepositoryMock.Setup(x => x.Update(It.IsAny<Dragon>())).Returns(fakeDragon);

            var dragonActor = Sys.ActorOf(Props.Create<DragonActor>(_dragonRepositoryMock.Object));

            //Act
            dragonActor.Tell(fakeGrowDragonCommand);

            ExpectNoMsg();
        }

        [Fact]
        public void DragonActor_Receives_Grow_Dragon_NonExistent_Id_Should_Throw()
        {
            //Arrange
            var fakeDragonId = 1;
            var fakeGrowDragonCommand = new GrowDragonCommand(fakeDragonId);
            Dragon nullDragon = null;

            _unitOfWorkMock.Setup(x => x.SaveChangesAsync(default))
                .Returns(Task.FromResult(1));

            _dragonRepositoryMock.SetupGet(x => x.UnitOfWork).Returns(_unitOfWorkMock.Object);
            _dragonRepositoryMock.Setup(x => x.GetAsync(It.IsAny<int>())).Returns(Task.FromResult(nullDragon));

            var dragonActor = Sys.ActorOf(Props.Create<DragonActor>(_dragonRepositoryMock.Object));

            var filter = CreateEventFilter(Sys);

            //Act
            dragonActor.Tell(fakeGrowDragonCommand);

            filter.Custom(logEvent => logEvent is Error).ExpectOne(() =>
            {
                Assert.True(true);
            });
        }
    }
}
