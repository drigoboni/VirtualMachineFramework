﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VirtualMachine.Data.DataContext;


#nullable disable

namespace VirtualMachine.Data.Migrations
{
    [DbContext(typeof(VendingMachineDbContext))]
    [Migration("20240522035813_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("VirtualMachineData.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Virtual Pet Cat",
                            Price = 29.90m,
                            Quantity = 8
                        },
                        new
                        {
                            Id = 2,
                            Name = "Virtual Pet Dog",
                            Price = 29.90m,
                            Quantity = 93
                        },
                        new
                        {
                            Id = 3,
                            Name = "Virtual Garden Kit",
                            Price = 2.90m,
                            Quantity = 10
                        },
                        new
                        {
                            Id = 4,
                            Name = "Virtual Music Album",
                            Price = 6.90m,
                            Quantity = 93
                        },
                        new
                        {
                            Id = 5,
                            Name = "Virtual Coffee",
                            Price = 1.90m,
                            Quantity = 69
                        },
                        new
                        {
                            Id = 6,
                            Name = "Virtual Energy Drink",
                            Price = 3.90m,
                            Quantity = 27
                        },
                        new
                        {
                            Id = 7,
                            Name = "Virtual Book",
                            Price = 5.90m,
                            Quantity = 18
                        },
                        new
                        {
                            Id = 8,
                            Name = "Virtual Plant",
                            Price = 2.90m,
                            Quantity = 76
                        },
                        new
                        {
                            Id = 9,
                            Name = "Virtual Adventure Pass",
                            Price = 12.90m,
                            Quantity = 6
                        },
                        new
                        {
                            Id = 10,
                            Name = "Virtual Art Piece",
                            Price = 99.90m,
                            Quantity = 67
                        },
                        new
                        {
                            Id = 11,
                            Name = "Virtual Game Token",
                            Price = 14.90m,
                            Quantity = 54
                        },
                        new
                        {
                            Id = 12,
                            Name = "Virtual Sunglasses",
                            Price = 10.90m,
                            Quantity = 64
                        });
                });
#pragma warning restore 612, 618
        }
    }
}