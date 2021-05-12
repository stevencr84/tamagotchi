using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tamagotchi.Domain.AggregatesModel.DragonAggregate;

namespace Tamagotchi.Infrastructure.EntityConfigurations
{
    public class LifeStageEntityTypeConfiguration : IEntityTypeConfiguration<LifeStage>
    {
        public void Configure(EntityTypeBuilder<LifeStage> lifeStagesConfiguration)
        {
            lifeStagesConfiguration.ToTable("lifestages", TamagotchiContext.DEFAULT_SCHEMA);

            lifeStagesConfiguration.HasKey(ls => ls.Id);

            lifeStagesConfiguration.Property(ls => ls.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            lifeStagesConfiguration.Property(ls => ls.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
