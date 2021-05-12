using Akka.Actor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Tamagotchi.Application.Application.Commands;
using Tamagotchi.Application.Application.Exceptions;
using Tamagotchi.Application.Application.Providers;
using Tamagotchi.Application.Application.Queries;

namespace Tamagotchi.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DragonController : ControllerBase
    {
        private readonly IActorRef _dragonActor;
        private readonly IDragonQueries _dragonQueries;
        private readonly ILogger<DragonController> _logger;
        public DragonController(IDragonActorProvider dragonActorProvider, IDragonQueries dragonQueries, ILogger<DragonController> logger)
        {
            _dragonActor = dragonActorProvider.Get();
            _dragonQueries = dragonQueries;
            _logger = logger;
        }

        [Route("create")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateDragonAsync([FromBody] CreateDragonCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.Name)) return BadRequest("Invalid name");

            try
            {
                var result = await _dragonActor.Ask(command);

                var basePath = this.Request?.Host;

                return Ok($"Your dragon was created sucessfully, to check on it use the following URI {basePath}/api/dragon/{result}");
            }
            catch(Exception ex)
            {
                _logger.LogError("Error creating dragon", ex.Message);

                throw;
            }

        }

        [Route("pet")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult PetDragon([FromBody] PetDragonCommand command)
        {
            if (command.DragonId <= 0) return BadRequest("Invalid dragon id");

            try
            {
                _dragonActor.Tell(command);

                return Ok("Your dragon is now happy");
            }
            catch(DragonNotFoundException ex)
            {
                _logger.LogError(ex.Message);

                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error petting dragon", ex.Message);
                throw;
            }
        }

        [Route("feed")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult FeedDragon([FromBody] FeedDragonCommand command)
        {
            if (command.DragonId <= 0) return BadRequest("Invalid dragon id");

            try
            {
                _dragonActor.Tell(command);

                return Ok("Your dragon is less hungry now");
            }
            catch (DragonNotFoundException ex)
            {
                _logger.LogError(ex.Message);

                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error feeding dragon", ex.Message);
                throw;
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DragonViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<IEnumerable<DragonViewModel>>> GetDragonsAsync()
        {
            try
            {
                var dragons = await _dragonQueries.GetDragonsAsync();

                return Ok(dragons);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error getting dragons", ex.Message);
                throw;
            }
        }

        [Route("{dragonId:int}")]
        [HttpGet]
        [ProducesResponseType(typeof(DragonViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> GetDragonAsync(int dragonId)
        {
            if (dragonId <= 0) return BadRequest("Invalid dragon id");

            try
            {
                var dragon = await _dragonQueries.GetDragonAsync(dragonId);

                return Ok(dragon);
            }
            catch (DragonNotFoundException ex)
            {
                _logger.LogError(ex.Message);

                return NotFound($"The dragon with id {dragonId} doesn't exist");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting dragon", ex.Message);

                throw;
            }
        }
    }
}