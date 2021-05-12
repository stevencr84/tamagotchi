using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tamagotchi.Domain.AggregatesModel.DragonAggregate;
using Tamagotchi.Domain.Common;

namespace Tamagotchi.Infrastructure.Repositories
{
    public class DragonRepository : IDragonRepository
    {
        private readonly TamagotchiContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public DragonRepository(TamagotchiContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Dragon Add(Dragon dragon)
        {
            if (dragon.IsTransient())
            {
                return _context.Dragons
                    .Add(dragon)
                    .Entity;
            }
            else
            {
                return dragon;
            }
        }

        public async Task<Dragon> GetAsync(int dragonId)
        {
            var dragon = await _context.Dragons
                .Where(pr => pr.Id == dragonId)
                .SingleOrDefaultAsync();

            return dragon;
        }

        public Dragon Update(Dragon dragon)
        {
            return _context.Dragons
            .Update(dragon)
            .Entity;
        }
    }
}
