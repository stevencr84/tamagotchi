using Akka.Actor;
using Akka.TestKit.Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamagotchi.Application.Application.Actors;
using Tamagotchi.Application.Application.Commands;
using Tamagotchi.Application.Application.Exceptions;
using Tamagotchi.Application.Application.Providers;
using Tamagotchi.Application.Application.Queries;
using Tamagotchi.Application.Controllers;
using Tamagotchi.Domain.AggregatesModel.DragonAggregate;
using Tamagotchi.Domain.Common;
using Xunit;

namespace Tamagotchi.UnitTests.Application
{
    public class TamagotchiApiTests : TestKit
    {
        private readonly Mock<IDragonActorProvider> _dragonActorProviderMock;
        private readonly Mock<IActorRef> _dragonActorMock;
        private readonly Mock<IDragonQueries> _dragonQueriesMock;
        private readonly Mock<ILogger<DragonController>> _loggerMock;
        private readonly Mock<IDragonRepository> _dragonRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public TamagotchiApiTests()
        {
            _dragonActorMock = new Mock<IActorRef>();
            _dragonQueriesMock = new Mock<IDragonQueries>();
            _loggerMock = new Mock<ILogger<DragonController>>();
            _dragonActorProviderMock = new Mock<IDragonActorProvider>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _dragonRepositoryMock = new Mock<IDragonRepository>();
        }

        [Fact]
        public async Task Create_Dragon_With_Valid_Command_Returns_Ok()
        {
            //Arrange
            var fakeCreateDragonCommand = new CreateDragonCommand("Test Dragon");
            var fakeDragon = new Dragon(fakeCreateDragonCommand.Name);

            _unitOfWorkMock.Setup(x => x.SaveChangesAsync(default))
                .Returns(Task.FromResult(1));

            _dragonRepositoryMock.SetupGet(x => x.UnitOfWork).Returns(_unitOfWorkMock.Object);
            _dragonRepositoryMock.Setup(x => x.Add(It.IsAny<Dragon>())).Returns(fakeDragon);
            
            var dragonActor = Sys.ActorOf(Props.Create<DragonActor>(_dragonRepositoryMock.Object));

            _dragonActorProviderMock.Setup(x => x.Get())
                .Returns(dragonActor);

            var dragonController = new DragonController(_dragonActorProviderMock.Object, 
                                                        _dragonQueriesMock.Object,
                                                        _loggerMock.Object);

            //Act
            var actionResult = await dragonController.CreateDragonAsync(fakeCreateDragonCommand) as OkObjectResult;

            //Assert
            Assert.Equal(actionResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task Create_Dragon_With_InvalidValid_Command_Returns_BadRequest()
        {
            //Arrange
            var fakeCreateDragonCommand = new CreateDragonCommand(string.Empty);

            var dragonController = new DragonController(_dragonActorProviderMock.Object,
                                                        _dragonQueriesMock.Object,
                                                        _loggerMock.Object);

            //Act
            var actionResult = await dragonController.CreateDragonAsync(fakeCreateDragonCommand) as BadRequestObjectResult;

            //Assert
            Assert.Equal(actionResult.StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public void Pet_Dragon_With_Valid_Command_Returns_Ok()
        {
            //Arrange
            var fakePetDragonCommand = new PetDragonCommand(1);

            _unitOfWorkMock.Setup(x => x.SaveChangesAsync(default))
                .Returns(Task.FromResult(1));

            _dragonRepositoryMock.SetupGet(x => x.UnitOfWork).Returns(_unitOfWorkMock.Object);
            _dragonRepositoryMock.Setup(x => x.Update(It.IsAny<Dragon>())).Returns(new Dragon("Test Dragon"));

            var dragonActor = Sys.ActorOf(Props.Create<DragonActor>(_dragonRepositoryMock.Object));

            _dragonActorProviderMock.Setup(x => x.Get())
                .Returns(dragonActor);

            var dragonController = new DragonController(_dragonActorProviderMock.Object,
                                                        _dragonQueriesMock.Object,
                                                        _loggerMock.Object);

            //Act
            var actionResult = dragonController.PetDragon(fakePetDragonCommand) as OkObjectResult;

            //Assert
            Assert.Equal(actionResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public void Pet_Dragon_With_InvalidValid_Command_Returns_BadRequest()
        {
            //Arrange
            var fakePetDragonCommand = new PetDragonCommand(0);

            var dragonController = new DragonController(_dragonActorProviderMock.Object,
                                                        _dragonQueriesMock.Object,
                                                        _loggerMock.Object);

            //Act
            var actionResult = dragonController.PetDragon(fakePetDragonCommand) as BadRequestObjectResult;

            //Assert
            Assert.Equal(actionResult.StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public void Feed_Dragon_With_Valid_Command_Returns_Ok()
        {
            //Arrange
            var fakeFeedDragonCommand = new FeedDragonCommand(1);

            _unitOfWorkMock.Setup(x => x.SaveChangesAsync(default))
                .Returns(Task.FromResult(1));

            _dragonRepositoryMock.SetupGet(x => x.UnitOfWork).Returns(_unitOfWorkMock.Object);
            _dragonRepositoryMock.Setup(x => x.Update(It.IsAny<Dragon>())).Returns(new Dragon("Test Dragon"));

            var dragonActor = Sys.ActorOf(Props.Create<DragonActor>(_dragonRepositoryMock.Object));

            _dragonActorProviderMock.Setup(x => x.Get())
                .Returns(dragonActor);

            var dragonController = new DragonController(_dragonActorProviderMock.Object,
                                                        _dragonQueriesMock.Object,
                                                        _loggerMock.Object);

            //Act
            var actionResult = dragonController.FeedDragon(fakeFeedDragonCommand) as OkObjectResult;

            //Assert
            Assert.Equal(actionResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public void Feed_Dragon_With_InvalidValid_Command_Returns_BadRequest()
        {
            //Arrange
            var fakeFeedDragonCommand = new FeedDragonCommand(0);

            var dragonController = new DragonController(_dragonActorProviderMock.Object,
                                                        _dragonQueriesMock.Object,
                                                        _loggerMock.Object);

            //Act
            var actionResult = dragonController.FeedDragon(fakeFeedDragonCommand) as BadRequestObjectResult;

            //Assert
            Assert.Equal(actionResult.StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Get_Dragons_Returns_List()
        {
            //Arrange
            DragonViewModel[] fakeResult = { new DragonViewModel() };
            _dragonQueriesMock.Setup(x => x.GetDragonsAsync()).Returns(Task.FromResult(fakeResult.AsEnumerable<DragonViewModel>()));

            var dragonController = new DragonController(_dragonActorProviderMock.Object,
                                                        _dragonQueriesMock.Object,
                                                        _loggerMock.Object);

            //Act
            var actionResult = await dragonController.GetDragonsAsync();

            //Assert
            actionResult.Result.As<OkObjectResult>().Value.Should().BeOfType(fakeResult.GetType());
        }

        [Fact]
        public async Task Get_Dragon_Returns_Dragon_Instance()
        {
            //Arrange
            var fakeDragonId = 1;
            var fakeDragon = new DragonViewModel();
            _dragonQueriesMock.Setup(x => x.GetDragonAsync(It.IsAny<int>())).Returns(Task.FromResult(fakeDragon));

            var dragonController = new DragonController(_dragonActorProviderMock.Object,
                                                        _dragonQueriesMock.Object,
                                                        _loggerMock.Object);

            //Act
            var actionResult = await dragonController.GetDragonAsync(fakeDragonId);

            //Assert
            actionResult.As<OkObjectResult>().Value.Should().BeOfType<DragonViewModel>();
        }

        [Fact]
        public async Task Get_Dragon_Inexistent_Id_Returns_NotFound()
        {
            //Arrange
            var fakeDragonId = 1;
            _dragonQueriesMock.Setup(x => x.GetDragonAsync(It.IsAny<int>())).Throws(new DragonNotFoundException());

            var dragonController = new DragonController(_dragonActorProviderMock.Object,
                                                        _dragonQueriesMock.Object,
                                                        _loggerMock.Object);

            //Act
            var actionResult = await dragonController.GetDragonAsync(fakeDragonId);

            //Assert
            actionResult.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task Get_Dragon_Invalid_Id_Returns_BadRequest()
        {
            //Arrange
            var fakeDragonId = 0;

            var dragonController = new DragonController(_dragonActorProviderMock.Object,
                                                        _dragonQueriesMock.Object,
                                                        _loggerMock.Object);

            //Act
            var actionResult = await dragonController.GetDragonAsync(fakeDragonId);

            //Assert
            actionResult.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
