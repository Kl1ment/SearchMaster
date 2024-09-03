﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SearchMaster.DataAccess;

#nullable disable

namespace SearchMaster.DataAccess.Migrations
{
    [DbContext(typeof(SearchMasterDbContext))]
    [Migration("20240825233816_Init1.3")]
    partial class Init13
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SearchMaster.DataAccess.Entities.CodeEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CodeHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Codes");
                });

            modelBuilder.Entity("SearchMaster.DataAccess.Entities.OrderEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(350)
                        .HasColumnType("character varying(350)");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("SearchMaster.DataAccess.Entities.PersonEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("character varying(13)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<float>("Rating")
                        .HasColumnType("real");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Persons");

                    b.HasDiscriminator().HasValue("PersonEntity");

                    b.UseTphMappingStrategy();

                    b.HasData(
                        new
                        {
                            Id = new Guid("40ba0839-2d41-4f55-902b-1850405caeb6"),
                            Email = "admin@gmail.com",
                            Name = "Климент",
                            Rating = 0f,
                            RoleId = 1,
                            Surname = "Иванов"
                        });
                });

            modelBuilder.Entity("SearchMaster.DataAccess.Entities.ReviewEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("HolderId")
                        .HasColumnType("uuid");

                    b.Property<int>("Mark")
                        .HasColumnType("integer");

                    b.Property<string>("TextData")
                        .IsRequired()
                        .HasMaxLength(350)
                        .HasColumnType("character varying(350)");

                    b.Property<Guid>("WriterId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("HolderId");

                    b.HasIndex("WriterId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("SearchMaster.DataAccess.Entities.RoleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Moderator"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Worker"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Client"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Registering"
                        },
                        new
                        {
                            Id = 6,
                            Name = "ConfirmingEmail"
                        });
                });

            modelBuilder.Entity("SearchMaster.DataAccess.Entities.ClientEntity", b =>
                {
                    b.HasBaseType("SearchMaster.DataAccess.Entities.PersonEntity");

                    b.HasDiscriminator().HasValue("ClientEntity");
                });

            modelBuilder.Entity("SearchMaster.DataAccess.Entities.WorkerEntity", b =>
                {
                    b.HasBaseType("SearchMaster.DataAccess.Entities.PersonEntity");

                    b.Property<string>("About")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<string>("Profession")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasDiscriminator().HasValue("WorkerEntity");
                });

            modelBuilder.Entity("SearchMaster.DataAccess.Entities.OrderEntity", b =>
                {
                    b.HasOne("SearchMaster.DataAccess.Entities.ClientEntity", "Client")
                        .WithMany("Orders")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("SearchMaster.DataAccess.Entities.PersonEntity", b =>
                {
                    b.HasOne("SearchMaster.DataAccess.Entities.RoleEntity", "RoleEntity")
                        .WithMany("Persons")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RoleEntity");
                });

            modelBuilder.Entity("SearchMaster.DataAccess.Entities.ReviewEntity", b =>
                {
                    b.HasOne("SearchMaster.DataAccess.Entities.PersonEntity", "Holder")
                        .WithMany("Reviews")
                        .HasForeignKey("HolderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SearchMaster.DataAccess.Entities.PersonEntity", "Writer")
                        .WithMany()
                        .HasForeignKey("WriterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Holder");

                    b.Navigation("Writer");
                });

            modelBuilder.Entity("SearchMaster.DataAccess.Entities.PersonEntity", b =>
                {
                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("SearchMaster.DataAccess.Entities.RoleEntity", b =>
                {
                    b.Navigation("Persons");
                });

            modelBuilder.Entity("SearchMaster.DataAccess.Entities.ClientEntity", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
