﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Offers.Infrastructure.Database;

#nullable disable

namespace Offers.Infrastructure.Database.Migrations
{
    [DbContext(typeof(OffersContext))]
    [Migration("20240321203747_Init1")]
    partial class Init1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("offers")
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Offers.Domain.GardenOffer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("Created");

                    b.Property<string>("CreatorId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("CreatorId");

                    b.Property<string>("CreatorName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("CreatorName");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Description");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("ExpirationDate");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("LastModified");

                    b.Property<string>("Recipient")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Recipient");

                    b.Property<int>("Status")
                        .HasColumnType("SMALLINT")
                        .HasColumnName("Status");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("numeric")
                        .HasColumnName("TotalPrice");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("GardenOffers", "offers");
                });

            modelBuilder.Entity("Offers.Domain.GardenOffer", b =>
                {
                    b.OwnsMany("Offers.Domain.GardenOfferItem", "_offerItems", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<string>("Code")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Code");

                            b1.Property<int>("GardenOfferId")
                                .HasColumnType("integer");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Name");

                            b1.Property<decimal>("Price")
                                .HasColumnType("numeric")
                                .HasColumnName("Price");

                            b1.Property<int>("Version")
                                .HasColumnType("integer");

                            b1.HasKey("Id");

                            b1.HasIndex("GardenOfferId");

                            b1.ToTable("GardenOfferItems", "offers");

                            b1.WithOwner()
                                .HasForeignKey("GardenOfferId");
                        });

                    b.Navigation("_offerItems");
                });
#pragma warning restore 612, 618
        }
    }
}