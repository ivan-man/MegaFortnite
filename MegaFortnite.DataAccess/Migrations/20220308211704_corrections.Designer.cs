﻿// <auto-generated />
using System;
using MegaFortnite.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MegaFortnite.DataAccess.Migrations
{
    [DbContext(typeof(MegaFortniteDbContext))]
    [Migration("20220308211704_corrections")]
    partial class corrections
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasPostgresExtension("uuid-ossp")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("MegaFortnite.Domain.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("LastName")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Phone")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("Email");

                    b.HasIndex("Phone");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Created = new DateTime(2022, 3, 8, 21, 17, 3, 971, DateTimeKind.Utc).AddTicks(9179),
                            Email = "test1@test.com",
                            FirstName = "Customer_1",
                            Phone = ""
                        },
                        new
                        {
                            Id = 2,
                            Created = new DateTime(2022, 3, 8, 21, 17, 3, 971, DateTimeKind.Utc).AddTicks(9503),
                            Email = "test2@test.com",
                            FirstName = "Customer_2",
                            Phone = ""
                        },
                        new
                        {
                            Id = 3,
                            Created = new DateTime(2022, 3, 8, 21, 17, 3, 971, DateTimeKind.Utc).AddTicks(9506),
                            Email = "test3@test.com",
                            FirstName = "Customer_3",
                            Phone = ""
                        });
                });

            modelBuilder.Entity("MegaFortnite.Domain.Models.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<string>("NickName")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<int>("Rate")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("WinRate")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Profiles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Created = new DateTime(2022, 3, 8, 21, 17, 3, 972, DateTimeKind.Utc).AddTicks(9393),
                            CustomerId = 1,
                            NickName = "xXx_predator_xXx",
                            Rate = 0,
                            WinRate = 0m
                        },
                        new
                        {
                            Id = 2,
                            Created = new DateTime(2022, 3, 8, 21, 17, 3, 972, DateTimeKind.Utc).AddTicks(9399),
                            CustomerId = 1,
                            NickName = "HArU6ATOP",
                            Rate = 0,
                            WinRate = 0m
                        },
                        new
                        {
                            Id = 3,
                            Created = new DateTime(2022, 3, 8, 21, 17, 3, 972, DateTimeKind.Utc).AddTicks(9400),
                            CustomerId = 1,
                            NickName = "4TO_C_E6AJIOM",
                            Rate = 0,
                            WinRate = 0m
                        });
                });

            modelBuilder.Entity("MegaFortnite.Domain.Models.Session", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LobbyKey")
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("integer");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("MegaFortnite.Domain.Models.SessionResult", b =>
                {
                    b.Property<Guid>("SessionId")
                        .HasColumnType("uuid");

                    b.Property<int>("GameProfileId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Score")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("SessionId", "GameProfileId");

                    b.HasIndex("GameProfileId");

                    b.HasIndex("SessionId");

                    b.ToTable("Results");
                });

            modelBuilder.Entity("MegaFortnite.Domain.Models.Profile", b =>
                {
                    b.HasOne("MegaFortnite.Domain.Models.Customer", "Customer")
                        .WithMany("Profiles")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("MegaFortnite.Domain.Models.Session", b =>
                {
                    b.HasOne("MegaFortnite.Domain.Models.Profile", "Owner")
                        .WithMany("Sessions")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("MegaFortnite.Domain.Models.SessionResult", b =>
                {
                    b.HasOne("MegaFortnite.Domain.Models.Profile", "GameProfile")
                        .WithMany()
                        .HasForeignKey("GameProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MegaFortnite.Domain.Models.Session", "Session")
                        .WithMany("Results")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GameProfile");

                    b.Navigation("Session");
                });

            modelBuilder.Entity("MegaFortnite.Domain.Models.Customer", b =>
                {
                    b.Navigation("Profiles");
                });

            modelBuilder.Entity("MegaFortnite.Domain.Models.Profile", b =>
                {
                    b.Navigation("Sessions");
                });

            modelBuilder.Entity("MegaFortnite.Domain.Models.Session", b =>
                {
                    b.Navigation("Results");
                });
#pragma warning restore 612, 618
        }
    }
}
