using Akka.Actor;
using Akka.Event;
using Akka.Logger.Serilog;
using System;
using Tamagotchi.Application.Application.Commands;
using Tamagotchi.Application.Application.Exceptions;
using Tamagotchi.Domain.AggregatesModel.DragonAggregate;

namespace Tamagotchi.Application.Application.Actors
{
    public class DragonActor : ReceiveActor
    {
        private readonly IDragonRepository _dragonRepository;
        private readonly ILoggingAdapter _log = Context.GetLogger<SerilogLoggingAdapter>();

        public DragonActor(IDragonRepository dragonRepository)
        {
            _dragonRepository = dragonRepository;

            ReceiveAsync<CreateDragonCommand>(async command =>
            {
                try
                {
                    var dragon = new Dragon(command.Name);

                    var savedDragon = _dragonRepository.Add(dragon);

                    await _dragonRepository.UnitOfWork.SaveChangesAsync();

                    _log.Info("{0} processed successfully", nameof(CreateDragonCommand));

                    Sender.Tell(savedDragon.Id);
                }
                catch(Exception ex)
                {
                    _log.Error("Error processing {0}, error message {1}", nameof(CreateDragonCommand), ex.Message);
                }
            });

            ReceiveAsync<PetDragonCommand>(async command =>
            {
                try
                {
                    var dragon = await _dragonRepository.GetAsync(command.DragonId);

                    if (dragon == null) throw new DragonNotFoundException($"The dragon with ID {command.DragonId} deson't exist");

                    dragon.Pet();

                    _dragonRepository.Update(dragon);

                    await _dragonRepository.UnitOfWork.SaveChangesAsync();

                    _log.Info("{0} processed successfully", nameof(PetDragonCommand));
                }
                catch (Exception ex)
                {
                    _log.Error("Error processing {0}, error message {1}", nameof(PetDragonCommand), ex.Message);
                }
            });

            ReceiveAsync<FeedDragonCommand>(async command =>
            {
                try
                {
                    var dragon = await _dragonRepository.GetAsync(command.DragonId);

                    if (dragon == null) throw new DragonNotFoundException($"The dragon with ID {command.DragonId} deson't exist");

                    dragon.Feed();

                    _dragonRepository.Update(dragon);

                    await _dragonRepository.UnitOfWork.SaveChangesAsync();

                    _log.Info("{0} processed successfully", nameof(FeedDragonCommand));
                }
                catch (Exception ex)
                {
                    _log.Error("Error processing {0}, error message {1}", nameof(FeedDragonCommand), ex.Message);
                }
            });

            ReceiveAsync<GrowDragonCommand>(async command =>
            {
                try
                {
                    var dragon = await _dragonRepository.GetAsync(command.DragonId);

                    if (dragon == null) throw new DragonNotFoundException($"The dragon with ID {command.DragonId} deson't exist");

                    dragon.Grows();

                    _dragonRepository.Update(dragon);

                    await _dragonRepository.UnitOfWork.SaveChangesAsync();

                    _log.Info("{0} processed successfully", nameof(GrowDragonCommand));
                }
                catch (Exception ex)
                {
                    _log.Error("Error processing {0}, error message {1}", nameof(GrowDragonCommand), ex.Message);
                }
            });
        }
    }
}
