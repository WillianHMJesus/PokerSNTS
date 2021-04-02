﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PokerSNTS.Infra.Data.Contexts;

namespace PokerSNTS.Infra.Data.Migrations
{
    [DbContext(typeof(PokerContext))]
    partial class PokerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PokerSNTS.Domain.Entities.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Actived")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("PokerSNTS.Domain.Entities.PlayerRound", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Actived")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PlayerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<short>("Position")
                        .HasColumnType("smallint");

                    b.Property<short>("Punctuation")
                        .HasColumnType("smallint");

                    b.Property<Guid>("RoundId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("RoundId");

                    b.ToTable("PlayersRounds");
                });

            modelBuilder.Entity("PokerSNTS.Domain.Entities.Ranking", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Actived")
                        .HasColumnType("bit");

                    b.Property<decimal?>("AwardValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Ranking");
                });

            modelBuilder.Entity("PokerSNTS.Domain.Entities.RankingPunctuation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Actived")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<short>("Position")
                        .HasColumnType("smallint");

                    b.Property<short>("Punctuation")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.ToTable("RankingPunctuations");
                });

            modelBuilder.Entity("PokerSNTS.Domain.Entities.Regulation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Actived")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(8000)
                        .HasColumnType("varchar(8000)");

                    b.HasKey("Id");

                    b.ToTable("Regulations");
                });

            modelBuilder.Entity("PokerSNTS.Domain.Entities.Round", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Actived")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("RankingId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RankingId");

                    b.ToTable("Rounds");
                });

            modelBuilder.Entity("PokerSNTS.Domain.Entities.PlayerRound", b =>
                {
                    b.HasOne("PokerSNTS.Domain.Entities.Player", "Player")
                        .WithMany("PlayersRounds")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PokerSNTS.Domain.Entities.Round", "Round")
                        .WithMany("PlayersRounds")
                        .HasForeignKey("RoundId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");

                    b.Navigation("Round");
                });

            modelBuilder.Entity("PokerSNTS.Domain.Entities.Round", b =>
                {
                    b.HasOne("PokerSNTS.Domain.Entities.Ranking", "Ranking")
                        .WithMany("Rounds")
                        .HasForeignKey("RankingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ranking");
                });

            modelBuilder.Entity("PokerSNTS.Domain.Entities.Player", b =>
                {
                    b.Navigation("PlayersRounds");
                });

            modelBuilder.Entity("PokerSNTS.Domain.Entities.Ranking", b =>
                {
                    b.Navigation("Rounds");
                });

            modelBuilder.Entity("PokerSNTS.Domain.Entities.Round", b =>
                {
                    b.Navigation("PlayersRounds");
                });
#pragma warning restore 612, 618
        }
    }
}
