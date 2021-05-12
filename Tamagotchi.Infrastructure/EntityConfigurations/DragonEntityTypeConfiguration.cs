using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamagotchi.Domain.AggregatesModel.DragonAggregate;

namespace Tamagotchi.Infrastructure.EntityConfigurations
{
    public class DragonEntityTypeConfiguration : IEntityTypeConfiguration<Dragon>
    {
        public void Configure(EntityTypeBuilder<Dragon> dragonConfiguration)
        {
            dragonConfiguration.ToTable("dragons", TamagotchiContext.DEFAULT_SCHEMA);

            dragonConfiguration.HasKey(p => p.Id);

            dragonConfiguration.Property(p => p.Id)
                .UseHiLo("dragonsseq", TamagotchiContext.DEFAULT_SCHEMA);

            dragonConfiguration
                .Property<string>("Name")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Name")
                .HasMaxLength(200)
                .IsRequired();

            dragonConfiguration
                .Property<int>("Age")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Age")
                .IsRequired();

            dragonConfiguration
                .Property<double>("Weight")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Weight")
                .IsRequired();

            dragonConfiguration
                .Property<int>("Happiness")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Happiness")
                .IsRequired();

            dragonConfiguration
                .Property<int>("Hunger")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Hunger")
                .IsRequired();

            dragonConfiguration
                .Property<int>("_lifeStageId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("LifeStageId")
                .IsRequired();

            dragonConfiguration.HasOne(d => d.LifeStage)
                .WithMany()
                .HasForeignKey("_lifeStageId");
        }
    }
}