using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading;
using System.Threading.Tasks;
using Tamagotchi.Domain.AggregatesModel.DragonAggregate;
using Tamagotchi.Domain.Common;
using Tamagotchi.Infrastructure.EntityConfigurations;

namespace Tamagotchi.Infrastructure
{
    public class TamagotchiContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "tamagotchi";
        public DbSet<Dragon> Dragons { get; set; }
        public DbSet<LifeStage> LifeStages { get; set; }

        public TamagotchiContext(DbContextOptions<TamagotchiContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LifeStageEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DragonEntityTypeConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await base.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}