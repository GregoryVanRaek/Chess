﻿// <auto-generated />
using System;
using ChessTournament.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ChessTournament.Infrastructure.Migrations
{
    [DbContext(typeof(DbContextChessTournament))]
    partial class DbContextChessTournamentModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ChessTournament.Domain.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Category", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Junior"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Veteran"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Senior"
                        });
                });

            modelBuilder.Entity("ChessTournament.Domain.Models.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<int>("Elo")
                        .HasColumnType("int");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Mail")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Member", null, t =>
                        {
                            t.HasCheckConstraint("CK_ELO", "ELO BETWEEN 0 AND 3000");
                        });
                });

            modelBuilder.Entity("ChessTournament.Domain.Models.Tournament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ActualRound")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("EloMax")
                        .HasColumnType("int");

                    b.Property<int?>("EloMin")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Place")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlayerMax")
                        .HasColumnType("int");

                    b.Property<int>("PlayerMin")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegistrationEndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("WomenOnly")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Tournament", null, t =>
                        {
                            t.HasCheckConstraint("CK_ELO_MAX", "ELOMAX IS NULL OR (ELOMAX BETWEEN 0 AND 3000)");

                            t.HasCheckConstraint("CK_ELO_MIN", "ELOMIN IS NULL OR (ELOMIN BETWEEN 0 AND 3000)");

                            t.HasCheckConstraint("CK_PLAYER_MAX", "PLAYERMAX BETWEEN 0 AND 32");

                            t.HasCheckConstraint("CK_PLAYER_MIN", "PLAYERMIN BETWEEN 0 AND 32");
                        });
                });

            modelBuilder.Entity("MM_Tournament_Category", b =>
                {
                    b.Property<int>("CategoriesId")
                        .HasColumnType("int");

                    b.Property<int>("TournamentsId")
                        .HasColumnType("int");

                    b.HasKey("CategoriesId", "TournamentsId");

                    b.HasIndex("TournamentsId");

                    b.ToTable("MM_Tournament_Category");
                });

            modelBuilder.Entity("MM_Tournament_Player", b =>
                {
                    b.Property<int>("MembersId")
                        .HasColumnType("int");

                    b.Property<int>("TournamentsId")
                        .HasColumnType("int");

                    b.HasKey("MembersId", "TournamentsId");

                    b.HasIndex("TournamentsId");

                    b.ToTable("MM_Tournament_Player");
                });

            modelBuilder.Entity("MM_Tournament_Category", b =>
                {
                    b.HasOne("ChessTournament.Domain.Models.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ChessTournament.Domain.Models.Tournament", null)
                        .WithMany()
                        .HasForeignKey("TournamentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MM_Tournament_Player", b =>
                {
                    b.HasOne("ChessTournament.Domain.Models.Member", null)
                        .WithMany()
                        .HasForeignKey("MembersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ChessTournament.Domain.Models.Tournament", null)
                        .WithMany()
                        .HasForeignKey("TournamentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
