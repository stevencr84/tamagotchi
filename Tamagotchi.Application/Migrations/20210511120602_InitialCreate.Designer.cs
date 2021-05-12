﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tamagotchi.Infrastructure;

namespace Tamagotchi.Application.Migrations
{
    [DbContext(typeof(TamagotchiContext))]
    [Migration("20210511120602_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.HasSequence("dragonsseq", "tamagotchi")
                .IncrementsBy(10);

            modelBuilder.Entity("Tamagotchi.Domain.AggregatesModel.DragonAggregate.Dragon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:HiLoSequenceName", "dragonsseq")
                        .HasAnnotation("SqlServer:HiLoSequenceSchema", "tamagotchi")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<int>("Age")
                        .HasColumnType("int")
                        .HasColumnName("Age");

                    b.Property<int>("Happiness")
                        .HasColumnType("int")
                        .HasColumnName("Happiness");

                    b.Property<int>("Hunger")
                        .HasColumnType("int")
                        .HasColumnName("Hunger");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("Name");

                    b.Property<double>("Weight")
                        .HasColumnType("float")
                        .HasColumnName("Weight");

                    b.Property<int>("_lifeStageId")
                        .HasColumnType("int")
                        .HasColumnName("LifeStageId");

                    b.HasKey("Id");

                    b.HasIndex("_lifeStageId");

                    b.ToTable("dragons", "tamagotchi");
                });

            modelBuilder.Entity("Tamagotchi.Domain.AggregatesModel.DragonAggregate.LifeStage", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("lifestages", "tamagotchi");
                });

            modelBuilder.Entity("Tamagotchi.Domain.AggregatesModel.DragonAggregate.Dragon", b =>
                {
                    b.HasOne("Tamagotchi.Domain.AggregatesModel.DragonAggregate.LifeStage", "LifeStage")
                        .WithMany()
                        .HasForeignKey("_lifeStageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LifeStage");
                });
#pragma warning restore 612, 618
        }
    }
}