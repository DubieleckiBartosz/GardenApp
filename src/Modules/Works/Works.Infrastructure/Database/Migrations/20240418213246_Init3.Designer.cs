﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Works.Infrastructure.Database;

#nullable disable

namespace Works.Infrastructure.Database.Migrations
{
    [DbContext(typeof(WorksContext))]
    [Migration("20240418213246_Init3")]
    partial class Init3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("works")
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Works.Domain.GardeningWorks.GardeningWork", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("BusinessId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("BusinessId");

                    b.Property<string>("ClientEmail")
                        .HasColumnType("text")
                        .HasColumnName("ClientEmail");

                    b.Property<DateTime?>("PlannedEndDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("PlannedEndDate");

                    b.Property<DateTime>("PlannedStartDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("PlannedStartDate");

                    b.Property<int>("Priority")
                        .HasColumnType("SMALLINT")
                        .HasColumnName("Priority");

                    b.Property<DateTime?>("RealEndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("RealStartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("SMALLINT")
                        .HasColumnName("Status");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.Property<string>("_tags")
                        .HasColumnType("text")
                        .HasColumnName("Tags");

                    b.HasKey("Id");

                    b.ToTable("GardeningWorks", "works");
                });

            modelBuilder.Entity("Works.Domain.WorkItems.Entities.TimeWeatherRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.Property<int>("WorkItemId")
                        .HasColumnType("integer");

                    b.Property<string>("_weathers")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Weathers");

                    b.HasKey("Id");

                    b.HasIndex("WorkItemId");

                    b.ToTable("TimeWeatherRecords", "works");
                });

            modelBuilder.Entity("Works.Domain.WorkItems.WorkItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("BusinessId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("BusinessId");

                    b.Property<DateTime?>("EstimatedEndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("EstimatedStartTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("GardeningWorkId")
                        .HasColumnType("integer")
                        .HasColumnName("GardeningWorkId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<int?>("RealTimeInMinutes")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("SMALLINT")
                        .HasColumnName("Status");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GardeningWorkId");

                    b.ToTable("WorkItems", "works");
                });

            modelBuilder.Entity("Works.Domain.GardeningWorks.GardeningWork", b =>
                {
                    b.OwnsOne("Works.Domain.GardeningWorks.ValueObjects.Location", "Location", b1 =>
                        {
                            b1.Property<int>("GardeningWorkId")
                                .HasColumnType("integer");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("City");

                            b1.Property<string>("NumberStreet")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("NumberStreet");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Street");

                            b1.HasKey("GardeningWorkId");

                            b1.ToTable("GardeningWorks", "works");

                            b1.WithOwner()
                                .HasForeignKey("GardeningWorkId");
                        });

                    b.Navigation("Location")
                        .IsRequired();
                });

            modelBuilder.Entity("Works.Domain.WorkItems.Entities.TimeWeatherRecord", b =>
                {
                    b.HasOne("Works.Domain.WorkItems.WorkItem", null)
                        .WithMany("TimeWeatherRecords")
                        .HasForeignKey("WorkItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Works.Domain.WorkItems.ValueObjects.TimeLog", "TimeLog", b1 =>
                        {
                            b1.Property<int>("TimeWeatherRecordId")
                                .HasColumnType("integer");

                            b1.Property<DateTime>("Created")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("TimeLogCreated");

                            b1.Property<DateTime>("EndDate")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("EndDate");

                            b1.Property<short>("Minutes")
                                .HasColumnType("smallint")
                                .HasColumnName("TimeLogMinutes");

                            b1.Property<DateTime>("StartDate")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("StartDate");

                            b1.HasKey("TimeWeatherRecordId");

                            b1.ToTable("TimeWeatherRecords", "works");

                            b1.WithOwner()
                                .HasForeignKey("TimeWeatherRecordId");
                        });

                    b.Navigation("TimeLog")
                        .IsRequired();
                });

            modelBuilder.Entity("Works.Domain.WorkItems.WorkItem", b =>
                {
                    b.HasOne("Works.Domain.GardeningWorks.GardeningWork", null)
                        .WithMany()
                        .HasForeignKey("GardeningWorkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Works.Domain.WorkItems.WorkItem", b =>
                {
                    b.Navigation("TimeWeatherRecords");
                });
#pragma warning restore 612, 618
        }
    }
}